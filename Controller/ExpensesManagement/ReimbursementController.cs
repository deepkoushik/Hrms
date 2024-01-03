using Kryptos.Hrms.API.Input_Models;
using Kryptos.Hrms.API.Input_Models.ExpensesManagement;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.ExpensesManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Security.Claims;

namespace Kryptos.Hrms.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly SendGridClient _sendGridClient;
        private readonly IReimbursementPro _reimbursementPro;
        private readonly KryptosHrmsDbContext _context;

        public ReimbursementController(IReimbursementPro reimbursementPro, KryptosHrmsDbContext context)
        {
            _sendGridClient = new SendGridClient("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");
            _reimbursementPro = reimbursementPro;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllReimbursements()
        {
            var reimbursements = _reimbursementPro.GetAllReimbursements();
            return Ok(reimbursements);
        }

        [HttpGet]
        public IActionResult GetReimbursementsForFTByDeptId(int departmentId)
        {
            try
            {
                // Check if the department with the specified ID exists
                if (!_context.Departments.Any(d => d.Id == departmentId))
                {
                    return NotFound("Dept ID does not exist.");
                }

                // Query the database to retrieve reimbursements based on the criteria
                var reimbursements = _context.Reimbursements
                    .Where(r => r.Employee.DepartmentId == departmentId &&
                                r.IsApprovedByManager == true &&
                                r.IsApprovedByHr == true &&
                                r.IsApprovedByFinanceTeam == null)
                    .Select(r => new
                    {
                        r.Id,
                        EmployeeName = r.Employee.FirstName + " " + r.Employee.LastName,
                        r.EmployeeId,
                        r.ReferenceCode,
                        r.BillDate,
                        r.CreatedTime,
                        r.BillPeriod,
                        r.Amount
                    })
                    .ToList();

                if (reimbursements.Count == 0)
                {
                    return NotFound("No such reimbursement found.");
                }

                return Ok(reimbursements);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while retrieving reimbursements.");
            }
        }


        [HttpGet]
        public IActionResult GetReimbursementsForHRByDeptId(int departmentId)
        {
            try
            {
                // Check if the department with the specified ID exists
                if (!_context.Departments.Any(d => d.Id == departmentId))
                {
                    return NotFound("Dept Id does not exist.");
                }

                // Query the database to retrieve reimbursements for the specified departmentId
                var reimbursements = _context.Reimbursements
                    .Where(r => r.Employee.DepartmentId == departmentId &&
                                r.IsApprovedByManager == true &&
                                r.IsApprovedByHr == null &&
                                r.IsApprovedByFinanceTeam == null)
                    .Select(r => new
                    {
                        r.Id,
                        EmployeeName = r.Employee.FirstName + " " + r.Employee.LastName,
                        r.EmployeeId,
                        r.ReferenceCode,
                        r.BillDate,
                        r.CreatedTime,
                        r.BillPeriod,
                        r.Amount,                        
                    })
                    .ToList();

                if (reimbursements.Count == 0)
                {
                    return NotFound("No such reimbursement found.");
                }

                return Ok(reimbursements);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while retrieving reimbursements.");
            }
        }





        [HttpGet]
        public IActionResult GetReimbursementsByEmployeeId(int employeeId)
        {
            try
            {
                // Query the database to get reimbursements for the specified employeeId
                var reimbursements = _context.Reimbursements
                    .Where(r => r.EmployeeId == employeeId)
                    .ToList();

                if (reimbursements.Count == 0)
                {
                    return NotFound($"No reimbursements found for EmployeeId {employeeId}");
                }

                return Ok(reimbursements);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpPost]
        public IActionResult AddReimbursement([FromForm] ReimbursementUploadModel inputmodel)
        {
            // Create a new Reimbursement object
            var reimbursement = new Reimbursement
            {
                Amount = inputmodel.Amount,
                EmployeeId = inputmodel.EmployeeId,
                BillDate = inputmodel.BillDate,
                BillNumber = inputmodel.BillNumber,
                BillPeriod = inputmodel.BillPeriod,
                CreatedBy = inputmodel.CreatedBy,
                CreatedTime = DateTime.Now,
                Description = inputmodel.Description,
                ReferenceCode = inputmodel.ReferenceCode,
                ReimbutsementTypeId = inputmodel.ReimbutsementTypeId,
                AppliedDate=DateTime.Now
                // Set other properties here...
            };

            if (inputmodel.BillDocument != null && inputmodel.BillDocument.Length > 0)
            {
                // Read the uploaded file into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    inputmodel.BillDocument.CopyTo(memoryStream);
                    reimbursement.BillDocument = memoryStream.ToArray();
                }
            }

            // Save the reimbursement to the database
            _reimbursementPro.AddReimbursement(reimbursement);

            // Update the isRequested property and save changes
             reimbursement.IsRequested = true;
            _reimbursementPro.UpdateReimbursement(reimbursement); // Assuming you have an UpdateReimbursement method

            return Ok("Created successfully");
        }


        [HttpPut]
        public async Task<IActionResult> ReimbursementApprovedByManager([FromBody] ReimbursementUpdateModel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound($"Id can't be empty");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest($"Reimbursement record not found");
                }

                // Update the specific columns

                existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                existingReimbursement.IsApprovedByManager = true;
                existingReimbursement.ApprovedDate = DateTime.Now;
                existingReimbursement.UpdateTime = DateTime.Now;
                existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("Reimbursement updated successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        public async Task<IActionResult> ReimbursementRejectedByManager([FromBody] ReimbursementUpdateModel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound($"Id can't be empty");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest($"Reimbursement record not found");
                }

                // Update the specific columns
                existingReimbursement.Reason = updateReimbursement.Reason;
                existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                existingReimbursement.IsApprovedByManager = false;
                existingReimbursement.ApprovedDate = null;
                existingReimbursement.UpdateTime = DateTime.Now;
                existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                // Fetch the employee's email from the Employee model based on the employee ID associated with the reimbursement
                var employee = await _context.Employees.FindAsync(existingReimbursement.EmployeeId);
                if (employee != null)
                {
                    // Send an email to the employee
                    await SendRejectEmailToEmployeeByManager(employee.EmailId);
                }


                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("This reimbursement has been rejected.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task SendRejectEmailToEmployeeByManager(string employeeEmail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "");
            var subject = "Reimbursement Approval status";
            var to = new EmailAddress(employeeEmail);
            var plainTextContent = "Your reimbursement has been rejected by Manager.";
            var htmlContent = "<strong>Your reimbursement has been rejected by the Manager.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }




        //[HttpPut]
        //public IActionResult UpdateReimbursement([FromForm] ReimbursementUploadModel inputModel)
        //{

        //    // Retrieve the existing reimbursement from the service or repository
        //    var existingReimbursement = _context.Reimbursements.FirstOrDefault(x =>x.Id == inputModel.Id);

        //    if (existingReimbursement == null)
        //    {
        //        return NotFound("Reimbursement Id not found");
        //    }

        //    // Update the properties of the existing reimbursement
        //    existingReimbursement.Amount = inputModel.Amount;
        //    existingReimbursement.UpdateTime = DateTime.Now;
        //    existingReimbursement.EmployeeId = inputModel.EmployeeId;
        //    existingReimbursement.AppliedDate = inputModel.AppliedDate;
        //    existingReimbursement.ApprovedById = inputModel.ApprovedById;
        //    existingReimbursement.ApprovedDate = inputModel.ApprovedDate;
        //    existingReimbursement.BillDate = inputModel.BillDate;
        //    existingReimbursement.BillNumber = inputModel.BillNumber;
        //    existingReimbursement.BillPeriod = inputModel.BillPeriod;
        //    existingReimbursement.Description = inputModel.Description;
        //    existingReimbursement.ReferenceCode = inputModel.ReferenceCode;
        //    existingReimbursement.ReimbutsementTypeId = inputModel.ReimbutsementTypeId;
        //    existingReimbursement.Status = inputModel.Status;

        //    if (inputModel.BillDocument != null && inputModel.BillDocument.Length > 0)
        //    {
        //        // Read the uploaded file into a byte array
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            inputModel.BillDocument.CopyTo(memoryStream);
        //            existingReimbursement.BillDocument = memoryStream.ToArray();
        //        }
        //    }

        //    // Update the reimbursement using the service or repository
        //    _reimbursementPro.UpdateReimbursement(existingReimbursement);

        //    return Ok("Updated Successfully");
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteReimbursement(int id)
        {
            try
            {
                _reimbursementPro.DeleteReimbursement(id);
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("No such reimbursement found");
            }
           
        }
        [HttpGet]
        public IActionResult GetReimbursementTypeById(int reimbursementTypeId)
        {
            // Fetch the reimbursement by ID including all fields
            var reimbursement = _context.Reimbursements
                .Where(r => r.ReimbutsementTypeId == reimbursementTypeId)
                .FirstOrDefault();

            if (reimbursement == null)
            {
                return NotFound("No such reimbursement type found");
            }

            return Ok(reimbursement);
        }
        [HttpGet]
        public IActionResult GetReimbursementsForManagerByDeptId(int departmentId)
        {
            try
            {
                // Check if the department with the specified ID exists
                if (!_context.Departments.Any(d => d.Id == departmentId))
                {
                    return NotFound("Department ID does not exist.");
                }

                // Query the database to retrieve reimbursements based on the criteria
                var reimbursements = _context.Reimbursements
                    .Where(r => r.Employee.DepartmentId == departmentId &&
                                r.IsApprovedByManager == null &&
                                r.IsApprovedByHr == null &&
                                r.IsApprovedByFinanceTeam == null &&
                                r.IsRequested == true)
                    .Select(r => new
                    {
                        r.Id,
                        EmployeeName = r.Employee.FirstName + " " + r.Employee.LastName,
                        r.EmployeeId,
                        r.ReferenceCode,
                        r.CreatedTime,
                        r.BillDate,
                        r.BillPeriod,
                        r.Amount,
                        r.ReimbutsementTypeId,
                    })
                    .ToList<object>();

                if (reimbursements.Count == 0)
                {
                    return NotFound("No reimbursements found for the specified department.");
                }

                return Ok(reimbursements);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while retrieving reimbursements.");
            }
        }


        [HttpPut]
        public async Task<IActionResult> ReimbursementApprovedByHR([FromBody] ReimbursementHRmodel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound($"Reimbursement record not found");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest($"Reimbursement record not found");
                }

                if (existingReimbursement.IsApprovedByManager==true)
                {
                    // Update the common columns
                    existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                    existingReimbursement.ApprovedDate = DateTime.Now;
                    existingReimbursement.UpdateTime = DateTime.Now;
                    existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                    // Set IsApprovedByHR to true
                    existingReimbursement.IsApprovedByHr = true;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok("Reimbursement approved successfully.");
                }
                else
                {
                    return BadRequest("Manager approval is mandatory.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ReimbursementRejectedByHR([FromBody] ReimbursementUpdateModel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound($"Reimbursement record not found");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest($"Reimbursement record not found");
                }
                existingReimbursement.Reason = updateReimbursement.Reason; // Update the common columns
                existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                existingReimbursement.ApprovedDate = null;
                existingReimbursement.UpdateTime = DateTime.Now;
                existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                // Set IsApprovedByHR to false
                existingReimbursement.IsApprovedByHr = false;

                var employee = await _context.Employees.FindAsync(existingReimbursement.EmployeeId);
                if (employee != null)
                {
                    // Send an email to the employee
                    await SendRejectEmailToEmployeeByHr(employee.EmailId);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("This reimbursement has been rejected.");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        private async Task SendRejectEmailToEmployeeByHr(string employeeEmail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
            var subject = "Reimbursement Approval status";
            var to = new EmailAddress(employeeEmail);
            var plainTextContent = "Your reimbursement has been rejected by the HR.";
            var htmlContent = "<strong>Your reimbursement has been rejected by the HR.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }


        [HttpPut]
        public async Task<IActionResult> ReimbursementApprovedByFinanceTeam([FromBody] ReimbursementFTModel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound("Reimbursement record not found");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest("Reimbursement record not found");
                }

                if (existingReimbursement.IsApprovedByHr == true)
                {
                    // Update the common columns
                    existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                    existingReimbursement.ApprovedDate = DateTime.Now;
                    existingReimbursement.UpdateTime = DateTime.Now;
                    existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                    // Set IsApprovedByFinanceTeam to true
                    existingReimbursement.IsApprovedByFinanceTeam = true;

                    // Fetch the employee's email from the Employee model based on the employee ID associated with the reimbursement
                    var employee = await _context.Employees.FindAsync(existingReimbursement.EmployeeId);
                    if (employee != null)
                    {
                        // Send an email to the employee
                        await SendApprovalEmailToEmployee(employee.EmailId);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok("Reimbursement approved successfully.");
                }
                else
                {
                    return BadRequest("HR approval is mandatory.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task SendApprovalEmailToEmployee(string employeeEmail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "Finance Team");
            var subject = "Reimbursement Approval status";
            var to = new EmailAddress(employeeEmail);
            var plainTextContent = "Your reimbursement has been approved by the finance team.";
            var htmlContent = "<strong>Your reimbursement has been approved by the finance team.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }

        [HttpPut]
        public async Task<IActionResult> ReimbursementRejectedByFinanceTeam([FromBody] ReimbursementUpdateModel updateReimbursement)
        {
            try
            {
                // Find the existing Reimbursement record
                var existingReimbursement = await _context.Reimbursements.FindAsync(updateReimbursement.Id);

                if (existingReimbursement == null)
                {
                    return NotFound($"Reimbursement record not found");
                }

                // Ensure the ID in the request matches the ID of the existing record
                if (updateReimbursement.Id != existingReimbursement.Id)
                {
                    return BadRequest($"Reimbursement record not found");
                }
                existingReimbursement.Reason = updateReimbursement.Reason; // Update the common columns
                existingReimbursement.ApprovedById = updateReimbursement.ApprovedById;
                existingReimbursement.ApprovedDate = null;
                existingReimbursement.UpdateTime = DateTime.Now;
                existingReimbursement.UpdatedBy = updateReimbursement.UpdatedBy;

                // Set IsApprovedByHR to false
                existingReimbursement.IsApprovedByFinanceTeam = false;

                var employee = await _context.Employees.FindAsync(existingReimbursement.EmployeeId);
                if (employee != null)
                {
                    // Send an email to the employee
                    await SendRejectEmailToEmployeeByFT(employee.EmailId);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("This reimbursement has been rejected.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        private async Task SendRejectEmailToEmployeeByFT(string employeeEmail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "Finance Team");
            var subject = "Reimbursement Approval status";
            var to = new EmailAddress(employeeEmail);
            var plainTextContent = "Your reimbursement has been rejected by the finance team.";
            var htmlContent = "<strong>Your reimbursement has been rejected by the finance team.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }


        [HttpGet("GetEmployeeNameById")]
        public IActionResult GetEmployeeNameById(int employeeId)
        {
            try
            {
                // Query the database to get the employee's first name and last name by ID
                var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

                if (employee != null)
                {
                    var employeeName = employee.FirstName + " " + employee.LastName;
                    return Ok(employeeName);
                }
                else
                {
                    return NotFound("Employee not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeName(int empid)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == empid);
            if (employee == null)
            {
                return NotFound(); // Return a 404 Not Found if the employee doesn't exist
            }

            var fullName = $"{employee.FirstName} {employee.LastName}";

            return Ok(new { id = employee.Id, name = fullName });
        }

        /*
        [HttpPost]
        public async Task<IActionResult> ApproveReimbursementMail(int reimbursementId)
        {
            try
            {
                // Retrieve the reimbursement record based on the given reimbursementId
                var reimbursement = await _context.Reimbursements.FindAsync(reimbursementId);

                if (reimbursement == null)
                {
                    return NotFound("Reimbursement record not found");
                }

                // Check if the reimbursement is approved by finance
                if (reimbursement.IsApprovedByFinanceTeam == true)
                {
                    // Retrieve the employee's email from your database
                    string employeeEmail = GetEmployeeEmail(reimbursement.EmployeeId);

                    // Create a SendGrid message
                    var from = new EmailAddress("suhitha.s@kryptostech.com", "Finance Team");
                    var to = new EmailAddress(employeeEmail);
                    var subject = "Reimbursement Approved";
                    var plainTextContent = "Your reimbursement has been approved!";
                    var htmlContent = "<strong>Your reimbursement has been approved!</strong>";
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                    // Send the email using SendGrid
                    var response = await _sendGridClient.SendEmailAsync(msg);

                    if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        return Ok("Reimbursement approved and email sent successfully.");
                    }
                    else
                    {
                        return BadRequest("Email sending failed.");
                    }
                }
                else
                {
                    return BadRequest("Reimbursement is not approved by finance.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        private string GetEmployeeEmail(int? employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee != null)
            {
                return employee.EmailId; // Assuming there's an 'Email' property in your Employee model.
            }
            else
            {
                // Handle the case where the employee with the given ID is not found.
                return null; // You might want to return an error message or throw an exception instead.
            }

        } */ 
    }
}
