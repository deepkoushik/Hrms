using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Kryptos.Hrms.API.Models;
using SendGrid;
using Microsoft.AspNetCore.Mvc;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly KryptosHrmsDbContext _dbContext;
    private readonly string _sendGridApiKey = "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";

    public AttendanceRepository(KryptosHrmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Attendance> CheckIn(int employeeId)
    {
        DateTime today = DateTime.Today;

        // Check if the employee has already checked in on the same day
        bool hasCheckedIn = await _dbContext.Attendances
            .AnyAsync(a => a.EmployeeId == employeeId &&
                           a.CheckInTime.HasValue &&
                           a.CheckInTime.Value.Date == DateTime.Now);

        if (hasCheckedIn)
        {
            throw new InvalidOperationException("You have already checked in today.");
        }

        DateTime now = DateTime.Now;
        TimeSpan checkInTime = now.TimeOfDay;
        int shiftTypeId;

        if (checkInTime >= new TimeSpan(18, 30, 0)) // 06:30 PM
        {
            // Check if the employee has already checked in for Night Shift on the same day
            hasCheckedIn = await _dbContext.Attendances
                .AnyAsync(a => a.EmployeeId == employeeId && a.CheckInTime.HasValue && a.CheckInTime.Value.Date >= DateTime.Now);

            if (hasCheckedIn)
            {
                throw new InvalidOperationException("You have already checked in today for night shift.");
            }

            shiftTypeId = 5; // Night Shift
                             // Set the check-in date to the next day for night shift employees
            today = today.AddDays(1);
            now = today.Add(checkInTime); // Set check-in time to the actual check-in time on the next day
        }
        else
        {
            DayOfWeek dayOfWeek = today.DayOfWeek;
            shiftTypeId = dayOfWeek switch
            {
                DayOfWeek.Monday => 2, // General Shift
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 2,
                DayOfWeek.Thursday => 2,
                DayOfWeek.Friday => 2,
                DayOfWeek.Saturday => 3, // WeekOff
                DayOfWeek.Sunday => 3,
                _ => throw new InvalidOperationException("Invalid day of the week.")
            };
        }

        Attendance newAttendance = new Attendance
        {
            EmployeeId = employeeId,
            CheckInTime = now,
            ShiftTypeId = shiftTypeId,
            CreatedBy = employeeId,
            CreatedTime = now,
            FirstHalfAttendance = true
        };

        if (shiftTypeId == 2) // General Shift
        {
            TimeSpan generalShiftStartTime = new TimeSpan(8, 0, 0); // Adjust the start time as needed
            TimeSpan generalShiftEndTime = new TimeSpan(17, 0, 0);  // Adjust the end time as needed

            if (checkInTime < generalShiftStartTime || checkInTime > generalShiftEndTime)
            {
                newAttendance.IsPresent = false;
            }
        }

        _dbContext.Attendances.Add(newAttendance);
        await _dbContext.SaveChangesAsync();

        return newAttendance;
    }






    public async Task<Attendance> CheckOut(int employeeId)
    {
        DateTime today = DateTime.Today;

        Attendance existingAttendance = await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId &&
                        a.CheckInTime.HasValue &&
                        a.CheckInTime.Value.Date == today &&
                        ((a.ShiftTypeId == 2) ||(a.ShiftTypeId == 5)))
            .FirstOrDefaultAsync();

        if (existingAttendance == null)
        {
            throw new InvalidOperationException("You have not checked in today.");
        }

        if (existingAttendance.CheckOutTime.HasValue)
        {
            throw new InvalidOperationException("You have already checked out today.");
        }

        DateTime now = DateTime.Now;
        TimeSpan checkOutTime = now.TimeOfDay;

        if (existingAttendance.ShiftTypeId == 5 && checkOutTime <= new TimeSpan(12, 0, 0))
        {
            existingAttendance.CheckOutTime = today.Add(checkOutTime);
        }
        else
        {
            existingAttendance.CheckOutTime = now;
        }

        // Ensure CheckOutTime is after CheckInTime
        if (existingAttendance.CheckOutTime < existingAttendance.CheckInTime)
        {
            existingAttendance.CheckOutTime = existingAttendance.CheckOutTime.Value.AddDays(1);
        }

        // Calculate working hours
        TimeSpan workingHours = existingAttendance.CheckOutTime.Value.TimeOfDay - existingAttendance.CheckInTime.Value.TimeOfDay;
        existingAttendance.WorkingHours = workingHours.TotalHours;

        // Set IsPresent flag based on working hours
        if (existingAttendance.ShiftTypeId == 5) // Night Shift
        {
            existingAttendance.IsPresent = workingHours.TotalHours > 9;
        }
        else
        {
            existingAttendance.IsPresent = workingHours.TotalHours >= 9;
        }

        _dbContext.Attendances.Update(existingAttendance);
        await _dbContext.SaveChangesAsync();

        return existingAttendance;
    }







    public List<Attendance> GetAttendanceDetails(int employeeId)
    {
        // Get the employee's department ID, role, and departmentId
        var employee = _dbContext.Employees
            .Where(e => e.Id == employeeId)
            .Select(e => new { e.DepartmentId, e.Role })
            .FirstOrDefault();

        if (employee == null)
        {
            throw new InvalidOperationException("Employee not found.");
        }

        // Check if the employee's role is 'Senior Manager' or 'HR Admin'
        if (!employee.Role.Contains("Senior Manager") && !employee.Role.Contains("HR Admin"))
        {
            throw new InvalidOperationException("Access restricted. Employee role does not allow access.");
        }

        // Get the attendance details for employees based on the role
        List<Attendance> attendanceDetails;

        if (employee.Role.Contains("Senior Manager"))
        {
            // Senior Manager can view their department employees' attendance
            attendanceDetails = _dbContext.Attendances.Where(a => a.Employee.DepartmentId == employee.DepartmentId &&
                            !(a.IsRequestForRegularization ?? false) &&
                            !(a.IsPresent ?? false)).ToList();
        }
        else if (employee.Role.Contains("HR Admin"))
        {
            // HR Admin can view all department employees' attendance
            attendanceDetails = _dbContext.Attendances.Where(a => !(a.IsRequestForRegularization ?? false) && !(a.IsPresent ?? false)).ToList();
        }
        else
        {
            throw new InvalidOperationException("Access restricted. Employee role does not allow access.");
        }

        // Automatically set IsPresent to false for weekdays with incomplete attendance records

        DateTime currentTime = DateTime.Now;
        DateTime startOfDay = currentTime.Date;

        foreach (var attendanceRecord in attendanceDetails)
        {
            if (attendanceRecord.AttendanceDate.HasValue &&
                attendanceRecord.AttendanceDate.Value >= startOfDay &&
                attendanceRecord.AttendanceDate.Value <= currentTime)
            {
                if (attendanceRecord.AttendanceDate.Value.DayOfWeek >= DayOfWeek.Monday &&
                    attendanceRecord.AttendanceDate.Value.DayOfWeek <= DayOfWeek.Friday)
                {
                    if (!(attendanceRecord.CheckInTime.HasValue && attendanceRecord.CheckOutTime.HasValue))
                    {
                        attendanceRecord.IsPresent = false;
                    }
                }
            }
        }

        return attendanceDetails; // Return the List<Attendance> as expected by the interface
    }






    public List<Attendance> GetAttendanceDetailsByEmployeeId(int employeeId)
    {
        // Retrieve attendance details for a specific employee by their ID
        return _dbContext.Attendances.Where(x => x.EmployeeId == employeeId).ToList();
            
    }

    public List<Attendance> GetCheckInTimesOfTheCurrentDate(int employeeId)
    {
        DateTime currentDate = DateTime.Today;
        return _dbContext.Attendances.Where(a => a.EmployeeId == employeeId && a.CheckInTime.HasValue && a.CheckInTime.Value.Date == currentDate)
            .ToList();
    }


    public async Task UpdateRegularizationByAttendanceId(int attendanceId,bool firstHalfAttendance,bool secondHalfAttendance, string reasonForRegularization)
    {
        var attendance = await _dbContext.Attendances.FindAsync(attendanceId);

        if (attendance == null)
        {
            throw new InvalidOperationException("Attendance record not found.");
        }

        // Check if CheckOut is NULL
        if (attendance.CheckOutTime == null)
        {
            attendance.IsPresent = false;

            attendance.IsRequestForRegularization = true;
            attendance.RegularizationStatus = false;
            attendance.UpdatedBy = attendance.EmployeeId;
            attendance.ReasonForRegularization = reasonForRegularization;
            attendance.FirstHalfAttendance = firstHalfAttendance;
            attendance.SecondHalfAttendance = secondHalfAttendance;
         

            _dbContext.Attendances.Update(attendance);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("You marked your check out time. Regularization not allowed.");
        }
    }

    public async Task<List<Attendance>> GetTheAttendanceListByEmployeeId(int employeeId)
    {
        var employee = await _dbContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();

        if (employee == null)
        {
            throw new InvalidOperationException("Employee not found.");
        }

        // Check if the employee's role contains 'Senior Manager' or is 'HR Admin'
        if (employee.Role.Contains("Senior Manager") || employee.Role.Contains("HR Admin"))
        {
            if (employee.Role.Contains("Senior Manager"))
            {
                // Senior Manager can view their department employees' attendance
                var list=_dbContext.Attendances.Where(x=>x.Employee.DepartmentId==employee.DepartmentId && x.IsRequestForRegularization == true).ToList();
                //return await _dbContext.Attendances
                //    .Where(a => a.DepartmentId == employee.DepartmentId && a.IsRequestForRegularization == true)
                //    .ToListAsync();
                return list;
            }
            else if (employee.Role.Contains("HR Admin"))
            {
                // HR Admin can view all department employees' attendance
                return await _dbContext.Attendances
                    .Include(a => a.Employee)
                    .Where(a => a.IsRequestForRegularization == true)
                    .ToListAsync();
            }
        }
        else
        {
            throw new InvalidOperationException("Access restricted. Employee role does not allow access.");
        }

        return new List<Attendance>();
    }



    public async Task UpdateRegularizationStatusAsRejectionBySeniorManager(int employeeId, int attendanceId, string lineManagerRejectionReason  ) 
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == employeeId)
            .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new InvalidOperationException("Employee not found.");
        }
        if (employee.Role.Contains("Senior Manager"))
        { 
            var attendance = await _dbContext.Attendances
                .Where (a => a.Id == attendanceId &&
                              a.Employee.DepartmentId == employee.DepartmentId &&
                              a.IsRequestForRegularization == true)
                .FirstOrDefaultAsync();



            var attendances = await _dbContext.Attendances
                   .Where(a => a.Id == attendanceId)
                   .Include(a => a.Employee) // Include the Employee navigation property if needed
                   .FirstOrDefaultAsync();
            

            if (attendance == null)
            {
                throw new InvalidOperationException("Attendance record not found, does not match employee's department, or regularization request is not applied.");
            }





            attendance.RegularizationStatus = false;
            attendance.IsPresent = false;
            attendance.ApprovedById = employeeId;
            attendance.LineManager = false;
            attendance.LineManagerRejectionReason = lineManagerRejectionReason;
           

            var employeeEmail = attendances.Employee.EmailId;



            // Get email IDs of employees who are Senior Managers and belong to the same department as the attendance record's employee
            //var seniorManagerEmails = await _dbContext.Employees
            //    .Where(e => e.Role.Contains("Senior Manager") && e.DepartmentId == attendance.Employee.DepartmentId)
            //    .Select(e => e.EmailId)
            //    .ToListAsync();

            var employeeFullName = attendances.Employee.FirstName + attendances.Employee.LastName;

            // Compose email content
            var subject = "Attendance Update";
            var plainTextContent = $"{employeeFullName} your Request for Regularization has been Rejected {lineManagerRejectionReason}.";
            var htmlContent = $"<p>{employeeFullName} your Request for Regularization has been Rejected {lineManagerRejectionReason}.</p>";

            // Create SendGridMessage without using MailHelper.CreateSingleEmail to avoid tracking links
            var msg = new SendGridMessage
            {
                From = new EmailAddress("suhitha.s@kryptostech.com", "HR Team"),
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent
            };

            // Add recipient email address
            msg.AddTo(new EmailAddress(employeeEmail));

            var sendGridClient = new SendGridClient(_sendGridApiKey);
            var response = await sendGridClient.SendEmailAsync(msg);

            _dbContext.Attendances.Update(attendance);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Access restricted. Only His Department Senior Managers can update regularization.");
        }
    }




 




    public async Task UpdateRegularizationStatusBySeniorManager(int employeeId, int attendanceId)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == employeeId)
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            throw new InvalidOperationException("Employee not found.");
        }

        // Check if the employee has the role "Senior Manager" or is "HR Admin"
        if (employee.Role.Contains("Senior Manager"))
        {
            var attendance = await _dbContext.Attendances
                .Where(a => a.Id == attendanceId &&
                            a.Employee.DepartmentId == employee.DepartmentId &&
                            a.IsRequestForRegularization == true)
                .FirstOrDefaultAsync();

            if (attendance == null)
            {
                throw new InvalidOperationException("Attendance record not found, does not match employee's department, or regularization request is not applied.");
            }

            attendance.RegularizationStatus = true;
            attendance.ApprovedById = employeeId;
            attendance.LineManager = true;
            attendance.IsPresent = true;

            var attendances = await _dbContext.Attendances
           .Where(a => a.Id == attendanceId)
           .Include(a => a.Employee) // Include the Employee navigation property if needed
           .FirstOrDefaultAsync();


            var employeeEmail = attendance.Employee.EmailId;
            var employeeFullName = attendance.Employee.FirstName + attendance.Employee.LastName;

            //compse Email

            var subject = "Attendance Update";
            var plainTextContent = $"{employeeFullName} Your Regularization Status Has Approved .";
            var htmlContent = $"<p>{employeeFullName}) Your Regularization Status Has Approved.</p>";

            //creating send grid mail

            var msgs = new SendGridMessage
            {
                From = new EmailAddress("suhitha.s@kryptostech.com", "HR Team"),
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent
            };

            msgs.AddTo(new EmailAddress(employeeEmail));

            var sendGridClient = new SendGridClient(_sendGridApiKey);
            var response = await sendGridClient.SendEmailAsync(msgs);

            // Check response status to ensure email delivery
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Sending email failed. Attendance update aborted.");
            }

            _dbContext.Attendances.Update(attendance);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Access restricted. Only His Department Senior Managers can update regularization.");
        }
    }












    public async Task PostAttendanceAutoAsync(int employeeId, DateTime attendanceDate)
    {
        // Check if attendance already exists for the given employee and date
        var existingAttendance = await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId && a.AttendanceDate == attendanceDate)
            .FirstOrDefaultAsync();

        if (existingAttendance == null)
        {
            // Create a new attendance record with the specified details
            var newAttendance = new Attendance
            {
                EmployeeId = employeeId,
                AttendanceDate = attendanceDate,
                IsPresent = false,
                IsAbsent = true,
                CreatedBy = employeeId,
                CreatedTime = DateTime.Now
            };

            _dbContext.Attendances.Add(newAttendance);
            await _dbContext.SaveChangesAsync();
        }
    }

   

    public async Task<List<Employee>> GetEmployeesWithoutAttendanceAsync(DateTime attendanceDate)
    {
        // Get a list of employees who haven't posted attendance for the given date
        var employeesWithoutAttendance = await _dbContext.Employees
            .Where(employee =>
                !_dbContext.Attendances.Any(attendance =>
                    attendance.EmployeeId == employee.Id &&
                    attendance.AttendanceDate == attendanceDate))
            .ToListAsync();

        return employeesWithoutAttendance;
    }




}





