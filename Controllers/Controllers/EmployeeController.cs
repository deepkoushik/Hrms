using Azure.Core;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Kryptos.Hrms.API.Provider;



namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //private ILog _logger;
        private readonly KryptosHrmsDbContext _context;
        private readonly IEmployeeProvider _provider;
        public readonly IEmployeeRepository _repo;
        public EmployeeController(IEmployeeProvider provider, IEmployeeRepository repo, KryptosHrmsDbContext context)
        {
            _context = context;
            _provider = provider;
            _repo = repo;
            //_logger = logger;
        }
        // Only HR Admin role can have access to create employee.
        // [Authorize(Roles = "HR Admin")]
        // It is checking the logged in user role and check the requirment role is matching or not.
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(CreateEmployee employee)
        {
            try
            {


                var createdByName = "HR Admin";




                var createdEmployee = _provider.CreateEmployee(employee , createdByName);
                //_logger.Information("Created Employee by HR Admin");
                return Ok(createdEmployee);
            }
            catch (CustomException ex)
            {
                //_logger.Error($"{ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the employee.");
            }
        }
        // Only HR Admin role can have access to Delete employee.
        //[Authorize(Roles = "HR Admin,Admin,Department Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpDelete]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            try
            {
                var existingEmployee = _provider.DeleteEmployee(id);
                return Ok(existingEmployee);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the employee.");
            }
        }
        // Only HR Admin role or Manager can have access to create employee.



        //[Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]



        //[Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpGet]
        public ActionResult<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = _provider.GetAllEmployees();
                return Ok(employees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the employee details.");
            }
        }

        // Only HR Admin or manager role can have access to create employee.
        //[Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpGet]
        public ActionResult<Employee> GetEmployeeByUsingAnyInputs(int? departmentId, string? deptName, int? managerId, string? job)
        {
            if (departmentId == null && deptName == null && managerId == null && job == null)
            {
                return BadRequest("Input field not to be null here");
            }
            try
            {
                List<Employee> employees = _provider.GetAllEmployeesByDepartmentOrManagerOrJob(departmentId, deptName, managerId, job);
                return Ok(employees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the employee details.");
            }
        }





        // Only HR Admin role or employeeor dept head can have access to create employee.
        //[Authorize(Roles ="HR Admin")]



        // Only HR Admin role or employeeor dept head can have access to create employee
        //[Authorize(Roles = "HR Admin")]



        // Only HR Admin role or employeeor dept head can have access to create employee.
        //  [Authorize(Roles ="HR Admin")]
        // Only HR Admin role or employeeor dept head can have access to create employee
        //[Authorize(Roles = "HR Admin")]



        //[Authorize(Roles = "HR Admin,Employee,Department Head")]
        // It is checking the logged in user role and check the requirmnt role is matching or not.
        [HttpPut]
        public ActionResult<Employee> UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee ,int updatedBy)
        { 
            try
            {
                
                var updatedDetails = _provider.UpdateEmployeeDetails(id, updatedEmployee,updatedBy);
                return Ok(updatedDetails);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while Updating the employee.");
            }
        }



        //[Authorize(Roles ="HR Admin")]





        //[//Authorize(Roles ="HR Admin")]



        [HttpGet]
        public ActionResult<Employee> GetActiveEmployees()
        {
            try
            {
                var activeEmployees = _provider.GetActiveEmployees();
                return Ok(activeEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the active employee.");
            }
        }
        // [Authorize(Roles = "HR Admin")]
        [HttpGet]
        public ActionResult<Employee> GetInActiveEmployees()
        {
            try
            {
                var inActiveEmployees = _provider.GetInActiveEmployees();
                return Ok(inActiveEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the inactive employee.");
            }
        }
        /*[Authorize(Roles = "HR Admin,Admin,Department Head,Functional Head")] */// Add appropriate authorization attributes
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> SearchEmployeeByFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return BadRequest("First name parameter cannot be empty.");
            }



            try
            {
                List<Employee> employees = _provider.GetAllEmployees(); // Assuming this method returns all employees
                var matchingEmployees = employees.Where(e => e.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)).ToList();



                if (matchingEmployees.Count == 0)
                {
                    return NotFound("No employees found with the given first name.");
                }



                return Ok(matchingEmployees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while searching employees by first name.");
            }
        }



        //[Authorize(Roles = "HR Admin,Department Head")]
        [HttpGet]
        public ActionResult<Employee> GetYourDepartmentEmployees()
        {
            try
            {
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var inActiveEmployees = _provider.GetYourDepartmentEmployees(userId);
                return Ok(inActiveEmployees);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting your department employees.");
            }
        }





        [HttpPut]



        [HttpPut]



        public ActionResult<SignInRequest> UpdatePassword(SignInRequest request)
        {



            try
            {
                var existingPassword = _provider.UpdatePassword(request);
                return Ok(existingPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateJob")]
        public ActionResult<Job> CreateJob(Job job)
        {
            var createdJob = _provider.CreateJob(job);
            return Created("post", createdJob);
        }
        [HttpGet("GetAllJobs")]
        public ActionResult<Job> GetAllJobs()
        {
            List<Job> jobs = _provider.GetAllJobs();
            return Ok(jobs);
        }




        [HttpGet]
        public IActionResult GetAllManagers()
        {
            try
            {
                var managers = _context.Employees
                    .Where(x => x.IsActive == true && x.Role == "Manager")
                    .Select(user => new
                    {
                        user.Id,
                        user.FirstName,
                        user.LastName
                    })
                    .ToList();



                if (managers.Count == 0)
                {
                    return NotFound("No Managers Found");
                }



                return Ok(managers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }











        //public ActionResult<ManagerInput> GetAllManager()
        //{
        //    try
        //    {
        //        var user = _context.Employees.FirstOrDefault(x => x.IsActive == true);
        //        if (user == null)
        //        {
        //            return NotFound("Manager not found");
        //        }
        //        return Ok(new
        //        {
        //            user.FirstName,
        //            user.LastName,
        //            user.ManagerId
        //        });
        //        //var manager = _provider.GetAllManager();
        //        //return Ok(manager);
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



    }
}