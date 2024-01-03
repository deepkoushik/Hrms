using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Numerics;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kryptos.Hrms.API.Repository.Attendance_LeaveClassRepository
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;
        private readonly string _sendGridApiKey = "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";
        public LeaveApplicationRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task PostLeaveApplicationAsync(int employeeId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo)
        {
            // Check if start date is greater than end date
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }

            // Check if the provided dates are in the past
            if (startDate < DateTime.Today || endDate < DateTime.Today)
            {
                throw new ArgumentException("Leave dates cannot be in the past.");
            }

            // Check if the start or end date falls on a Saturday or Sunday
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday ||
                endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException("Weekend dates (Saturday or Sunday) are not allowed for leave.");
            }

            bool leaveAlreadyExists = await _dbContext.LeaveApplications
       .AnyAsync(la => la.EmployeeId == employeeId &&
                       la.StartDate == startDate &&
                       la.EndDate == endDate);

            if (leaveAlreadyExists)
            {
                throw new InvalidOperationException("Leave with the same startDate and endDate already exists for the specified employee.");
            }

            // Fetch LeaveAvailability for the specified employee
            var leaveAvailability = await _dbContext.LeaveAvailabilities
                .FirstOrDefaultAsync(la => la.EmployeeId == employeeId);

            if (leaveAvailability == null)
            {
                throw new InvalidOperationException("LeaveAvailability not found for the specified employee.");
            }

            // Check if the EmployeeId has a value in the LeaveAvailability
            if (leaveAvailability.EmployeeId == null)
            {
                throw new InvalidOperationException("EmployeeId has no value in LeaveAvailability.");
            }

            // Determine the appropriate LeaveAvailability columns based on the LeaveType
            int? leaveBalanceToUpdate = leaveType switch
            {
                "Casual" when leaveAvailability.CasualLeaveBalance > 0 => leaveAvailability.CasualLeaveBalance,
                "Sick" when leaveAvailability.SickLeaveBalance > 0 => leaveAvailability.SickLeaveBalance,
                "Compensatory" when leaveAvailability.ComponsetoryLeaveBalance > 0 => leaveAvailability.ComponsetoryLeaveBalance,
                _ => null
            };

            // Check if the employee has enough leave balance
            if (leaveBalanceToUpdate.GetValueOrDefault() <= 0)
            {
                throw new InvalidOperationException("You have no more leaves.");
            }

            // Update the LeaveAvailability columns
            switch (leaveType)
            {
                case "Casual":
                    leaveAvailability.CasualLeaveBalance--;
                    leaveAvailability.TotalLeaveBalance--;
                    break;
                case "Sick":
                    leaveAvailability.SickLeaveBalance--;
                    leaveAvailability.TotalLeaveBalance--;
                    break;
                case "Compensatory":
                    leaveAvailability.ComponsetoryLeaveBalance--;
                    leaveAvailability.TotalLeaveBalance--;
                    break;
                default:
                    throw new ArgumentException("Invalid LeaveType.");
            }

            string alternateMobileStr = alternateMobileNo.ToString();
            if (!Regex.IsMatch(alternateMobileStr, @"^[0-9]{10}$"))
            {
                throw new ArgumentException("Alternate mobile number must be a valid 10-digit number with no letters or special characters.");
            }



            // Create LeaveApplication object
            LeaveApplication leaveApplication = new LeaveApplication
            {
                EmployeeId = employeeId,
                LeaveType = leaveType, // Assuming LeaveType is a property in LeaveApplication
                StartDate = startDate,
                EndDate = endDate,
                LeaveReason = leaveReason,
                IsRequested = true,
                CreatedBy = employeeId, // Set CreatedBy to employeeId provided by the user
                CreatedTime = DateTime.Now,
                FromFirstHalf = fromFirstHalf,
                FromSecondHalf = fromSecondHalf,
                FromFullDay = fromFullDay,
                ToFirstHalfDay = toFirstHalfDay,
                Medical = medical,
                ToSecondHalfDay = toSecondHalfDay,
                AlternateMobileNo = alternateMobileNo

            };

            // Save changes to the database
            _dbContext.LeaveApplications.Add(leaveApplication);
            await _dbContext.SaveChangesAsync();
        }



        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(int employeeId)
        {
            // Retrieve employee's role based on employeeId
            string role = await _dbContext.Employees
                .Where(e => e.Id == employeeId)
                .Select(e => e.Role)
                .FirstOrDefaultAsync();

            if (role != null)
            {
                if (role.Contains("HR Admin"))
                {
                    // Retrieve all employee's leave applications
                    return await _dbContext.LeaveApplications.ToListAsync();
                }
                else if (role.Contains("Senior Manager"))
                {
                    // Retrieve departmentId based on employeeId
                    int? departmentId = await _dbContext.Employees
                        .Where(e => e.Id == employeeId)
                        .Select(e => e.DepartmentId)
                        .FirstOrDefaultAsync();

                    if (departmentId.HasValue)
                    {
                        // Retrieve leave applications for employees in the same department
                        return await _dbContext.LeaveApplications
                            .Where(l => _dbContext.Employees.Any(e => e.DepartmentId == departmentId && e.Id == l.EmployeeId))
                            .ToListAsync();
                    }
                    else
                    {
                        // Handle the case where departmentId is not available for the Senior Manager
                        return Enumerable.Empty<LeaveApplication>();
                    }
                }
                else
                {
                    // Throw an exception for other roles or unknown roles
                    throw new InvalidOperationException($"Employee with Id {employeeId} does not have the required role.");
                }
            }

            // Handle the case where role is null (employee not found)
            throw new InvalidOperationException($"Employee with Id {employeeId} not found.");
        }

        public async Task UpdateLeaveApplicationAsync(int leaveApplicationId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo)
        {
           

            if (startDate >= endDate)
            {
                throw new InvalidOperationException("Start date must be less than end date.");
            }

            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday ||
                endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new InvalidOperationException("Start date and end date must not be on weekends.");
            }

            var existingLeaveApplication = await _dbContext.LeaveApplications.FindAsync(leaveApplicationId);


            if (existingLeaveApplication == null)
            {
                throw new InvalidOperationException($"Leave application with Id {leaveApplicationId} not found.");
            }

            if (existingLeaveApplication.Id != leaveApplicationId)
            {
                throw new InvalidOperationException($"Leave application with Id {leaveApplicationId} does not belong to the specified employee.");
            }

            string alternateMobileStr = alternateMobileNo.ToString();
            if (!Regex.IsMatch(alternateMobileStr, @"^[0-9]{10}$"))
            {
                throw new ArgumentException("Alternate mobile number must be a valid 10-digit number with no letters or special characters.");
            }

            // Update leave application details
            existingLeaveApplication.LeaveType = leaveType;
            existingLeaveApplication.StartDate = startDate;
            existingLeaveApplication.EndDate = endDate;
            existingLeaveApplication.LeaveReason = leaveReason;
            existingLeaveApplication.FromFirstHalf = fromFirstHalf;
            existingLeaveApplication.FromSecondHalf = fromSecondHalf;
            existingLeaveApplication.FromFullDay = fromFullDay;
            existingLeaveApplication.ToFirstHalfDay = toFirstHalfDay;
            existingLeaveApplication.ToSecondHalfDay = toSecondHalfDay;
            existingLeaveApplication.Medical = medical;
            existingLeaveApplication.AlternateMobileNo = alternateMobileNo;

            // Update other properties as needed

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LeaveApplication>> GetLeaveApplicationsOfEmployeeAsync(int employeeId)
        {
            return await _dbContext.LeaveApplications
                .Where(l => l.EmployeeId == employeeId)
                .ToListAsync();
        }



        public async Task UpdateLeaveApplicationStatusAsRejectionBySeniorManager(int employeeId, int leaveApplicationId, string leaveRejectionReason)
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
                var leaveApplication = await _dbContext.LeaveApplications
                    .Where(l => l.Id == leaveApplicationId &&
                                  l.Employee.DepartmentId == employee.DepartmentId &&
                                  l.LeaveStatus == false)
                    .Include(l => l.Employee) // Include the Employee navigation property if needed
                    .FirstOrDefaultAsync();

                if (leaveApplication == null || leaveApplication.Employee == null)
                {
                    throw new InvalidOperationException("Leave Application not found, or Leave request is not applied.");
                }

                // Retrieve the corresponding LeaveAvailability record
                var leaveAvailability = await _dbContext.LeaveAvailabilities
                    .FirstOrDefaultAsync(la => la.EmployeeId == leaveApplication.EmployeeId);

                if (leaveAvailability == null)
                {
                    throw new InvalidOperationException($"LeaveAvailability not found for the specified employee (ID: {leaveApplication.EmployeeId}).");
                }

                // Increment LeaveAvailability based on LeaveType
                switch (leaveApplication.LeaveType)
                {
                    case "Casual":
                        leaveAvailability.CasualLeaveBalance++;
                        leaveAvailability.TotalLeaveBalance++;
                        break;
                    case "Sick":
                        leaveAvailability.SickLeaveBalance++;
                        leaveAvailability.TotalLeaveBalance++;
                        break;
                    case "Compensatory":
                        leaveAvailability.ComponsetoryLeaveBalance++;
                        leaveAvailability.TotalLeaveBalance++;
                        break;
                    default:
                        // Handle other leave types if necessary
                        break;
                }

                // Update LeaveApplication status and rejection reason
                leaveApplication.LeaveStatus = false;
                leaveApplication.LeaveRejectionReason = leaveRejectionReason;

                var employeeEmail = leaveApplication.Employee.EmailId;

                var employeeFullName = leaveApplication.Employee.FirstName + leaveApplication.Employee.LastName;

                // Compose email content
                var subject = "Leave Application Update";
                var plainTextContent = $"{employeeFullName} your Request for LeaveApplication has been Rejected {leaveRejectionReason}.";
                var htmlContent = $"<p>{employeeFullName} your Request for LeaveApplication has been Rejected {leaveRejectionReason}.</p>";

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

                _dbContext.LeaveApplications.Update(leaveApplication);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Access restricted. Only His Department Senior Managers can Reject.");
            }
        }











        public async Task UpdateLeaveApplicationStatusBySeniorManager(int employeeId, int leaveApplicationId)
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
                var leaveApplication = await _dbContext.LeaveApplications
                    .Where(l => l.Id == leaveApplicationId &&
                                l.Employee.DepartmentId == employee.DepartmentId &&
                                l.LeaveStatus == false)
                    .FirstOrDefaultAsync();

                if (leaveApplication == null)
                {
                    throw new InvalidOperationException("Attendance record not found, does not match employee's department, or regularization request is not applied.");
                }

                if (!string.IsNullOrEmpty(leaveApplication.LeaveRejectionReason))
                {
                    throw new InvalidOperationException("The Leave Application cannot be Approved because it has been rejected. Reason: " + leaveApplication.LeaveRejectionReason);
                }

                leaveApplication.LineManager = true;
                leaveApplication.LeaveStatus = true;
                leaveApplication.IsApproved = true;
                leaveApplication.ApproverId = employeeId;
       

                var leaveApplications = await _dbContext.LeaveApplications
               .Where(l => l.Id == leaveApplicationId)
               .Include(l => l.Employee) // Include the Employee navigation property if needed
               .FirstOrDefaultAsync();


                var employeeEmail = leaveApplication.Employee.EmailId;
                var employeeFullName = leaveApplication.Employee.FirstName + leaveApplication.Employee.LastName;

                //compse Email

                var subject = "Leave Application Update";
                var plainTextContent = $"{employeeFullName} Your Leave Application Status Has Approved .";
                var htmlContent = $"<p>{employeeFullName}) Your Leave Application Status Has Approved.</p>";

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
                    throw new Exception("Sending email failed. leave status update aborted.");
                }

                _dbContext.LeaveApplications.Update(leaveApplication);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Access restricted. Only His Department Senior Managers can update regularization.");
            }
        }




    }
}
