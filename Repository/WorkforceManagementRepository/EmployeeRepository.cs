using Azure.Core;
using Kryptos.Hrms.API.Functions;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider;



using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly KryptosHrmsDbContext _context;
        public EmployeeRepository(KryptosHrmsDbContext context)
        {
            _context = context;
        }
        public Employee CreateEmployee(CreateEmployee employee, double ctcpy)
        {
            Employee newEmployee = new Employee();
            var empCheck = _context.Employees.FirstOrDefault(x => (x.EmailId == employee.EmailId || x.PhoneNo == employee.PhoneNumber) && x.IsActive == true);
            if (empCheck == null)
            {
                string spclChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
                var nums = "1234567890";
                if (!(employee.FirstName.Contains(spclChar) || employee.FirstName.Contains(nums) || employee.LastName.Contains(spclChar) || employee.LastName.Contains(nums)))
                {
                    if (!(employee.Password.Contains(spclChar) && employee.Password.Contains(nums) && employee.Password.Length > 7 && employee.Password.Length > 16))
                    {
                        CreatePasswordhash(employee.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                        newEmployee.FirstName = employee.FirstName;
                        newEmployee.LastName = employee.LastName;
                        //newEmployee.Password = Encrypt(employee.Password);
                        newEmployee.PasswordHash = PasswordHash;
                        newEmployee.PasswordSalt = PasswordSalt;
                        newEmployee.CreatedTime = DateTime.Now;
                        newEmployee.DepartmentId = employee.DepartmentId;
                        newEmployee.EmailId = employee.EmailId;
                        newEmployee.HireDate = employee.HireDate;
                        newEmployee.IsActive = true;
                        newEmployee.JobId = employee.JobId;
                        newEmployee.ManagerId = employee.ManagerId;
                        newEmployee.Role = employee.Roles;
                        newEmployee.PhoneNo = employee.PhoneNumber;
                        EmployeeFunction.SetLeaveAvailabilities(newEmployee, 1, 1);
                        var employeeSalaryFunctionRepository = new EmployeeSalaryFunctionRepository(_context);
                        employeeSalaryFunctionRepository.SetEmployeeSalary(newEmployee, ctcpy);





                        _context.Employees.Add(newEmployee);
                        _context.SaveChanges();
                        return newEmployee;
                    }
                    else
                    {
                        throw new CustomException("Password must be alphanumeric and must contain a special character with a length between 8-15");
                    }
                }
                else
                {
                    throw new CustomException("Special characters are not allowed in the name");
                }
            }
            else
            {
                throw new CustomException("Email or phone number already exists. Please try again");
            }
        }



        public Job CreateJob(Job job)
        {
            try
            {
                var job1 = _context.Jobs.FirstOrDefault(x => x.Name == job.Name);
                if (job1 == null)
                {
                    _context.Jobs.Add(job);
                    _context.SaveChanges();
                    return job;
                }
                else
                {
                    throw new CustomException("JobTitle already exist");
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while creating the job.", ex);
            }
        }



        public Employee DeleteEmployee(int id)
        {
            if (id > 0)
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id && x.IsActive == true);
                if (employee != null)
                {
                    employee.IsActive = false;
                    _context.SaveChanges();
                    return employee;
                }
                else
                {
                    throw new CustomException("Employee doesn't exist");
                }
            }
            else
            {
                throw new CustomException("Invalid employee ID");
            }
        }



        public List<Employee> GetActiveEmployees()
        {
            try
            {
                var activeEmployees = _context.Employees.Where(x => x.IsActive == true).ToList();
                return activeEmployees;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while retrieving the active employees.", ex);
            }
        }



        public List<Employee> GetAllEmployees()
        {
            try
            {
                var employees = _context.Employees.Where(x => x.IsActive == true).ToList();
                return employees;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while retrieving all employees.", ex);
            }
        }



        public List<Employee> GetAllEmployeesByDepartmentOrManagerOrJob(int? departmentId, string? deptName, int? managerId, string? job)
        {
            if (departmentId != null && departmentId > 0)
            {
                return _context.Employees.Where(x => x.Id == departmentId && x.IsActive == true).ToList();
            }
            else if (managerId != null && managerId > 0)
            {
                return _context.Employees.Where(x => x.ManagerId == managerId && x.IsActive == true).ToList();
            }
            else if (!string.IsNullOrEmpty(job))
            {
                var jobs = _context.Jobs.FirstOrDefault(m => m.Name.Contains(job));
                return _context.Employees.Where(x => x.JobId == jobs.Id && x.IsActive == true).ToList();
            }
            else if (!string.IsNullOrEmpty(deptName))
            {
                var dept = _context.Departments.FirstOrDefault(x => x.Name.Contains(deptName));
                return _context.Employees.Where(x => x.Id == dept.Id && x.IsActive == true).ToList();
            }
            else
            {
                throw new CustomException("Input is empty. Please enter any field");
            }
        }



        public List<Job> GetAllJobs()
        {
            try
            {
                return _context.Jobs.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while retrieving all jobs.", ex);
            }
        }



        public Employee GetEmployee(string id)
        {
            if (id != null)
            {
                int result = int.Parse(id);
                var employee = _context.Employees.FirstOrDefault(x => x.Id == result && x.IsActive == true);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new CustomException("Employee doesn't exist");
                }
            }
            else
            {
                throw new CustomException("ID can't be null");
            }
        }



        public List<Employee> GetInActiveEmployees()
        {
            try
            {
                var inActiveEmployees = _context.Employees.Where(x => x.IsActive == false).ToList();
                return inActiveEmployees;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while retrieving the inactive employees.", ex);
            }
        }



        public List<Employee> GetYourDepartmentEmployees(int userId)
        {
            var manager = _context.Employees.Find(userId);
            var deptEmployees = _context.Employees.Where(x => x.Id == manager.Id && x.IsActive == true).ToList();
            return deptEmployees;
        }

        public Employee UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee, int updatedBy)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(i => i.Id == id && i.IsActive == true);
            if (existingEmployee != null)
            {
                if (updatedEmployee.Password != null)
                {
                    CreatePasswordhash(updatedEmployee.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                    existingEmployee.PasswordHash = PasswordHash;
                    existingEmployee.PasswordSalt = PasswordSalt;
                }
                existingEmployee.UpdatedBy = updatedBy;
                if (updatedEmployee.FirstName != null)
                {
                    existingEmployee.FirstName = updatedEmployee.FirstName;
                }
                existingEmployee.LastName = updatedEmployee.LastName;
                if (updatedEmployee.EmailId != null)
                {
                    existingEmployee.EmailId = updatedEmployee.EmailId;
                }
                if (updatedEmployee.PhoneNumber != null)
                {
                    existingEmployee.PhoneNo = updatedEmployee.PhoneNumber;
                }
                _context.SaveChanges();
                return existingEmployee;
            }
            else
            {
                throw new CustomException("Enter any values to update the employee");
            }
        }
        // Password Encryption.
        /*   public string Encrypt(string plainText)
           {
               if (plainText == null) throw new CustomException("password is not null here");

 

               // Encrypt data
               var data = Encoding.Unicode.GetBytes(plainText);
               byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
               // Return as base64 string
               return Convert.ToBase64String(encrypted);
           }
           // Password Decryption.
           public string Decrypt(string ciphen)
           {
               if (ciphen == null) throw new CustomException("password is not null here");

 

               // Parse base64 string
               byte[] data = Convert.FromBase64String(ciphen);

 

               // Decrypt data
               byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
               return Encoding.Unicode.GetString(decrypted);
           }*/
        public void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public SignInRequest UpdatePassword(SignInRequest request)
        {
            var empCheck = _context.Employees.FirstOrDefault(x => x.EmailId == request.Email && x.IsActive == true);
            if (empCheck != null)
            {
                CreatePasswordhash(request.password, out byte[] PasswordHash, out byte[] PasswordSalt);
                empCheck.PasswordHash = PasswordHash;
                empCheck.PasswordSalt = PasswordSalt;
            }
            else
            {
                throw new Exception("Email Id not found");
            }
            _context.SaveChanges();
            return request;
        }





    }
}