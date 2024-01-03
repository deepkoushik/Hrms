using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers.InterfaceAttendance_LeaveProvider;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly ILeaveApplicationProvider _leaveApplicationProvider;
        private readonly string _sendGridApiKey = "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";

        public LeaveApplicationController(ILeaveApplicationProvider leaveApplicationProvider)
        {
            _leaveApplicationProvider = leaveApplicationProvider;
        }

        [HttpPost("Post Leave Application")]
        public async Task<IActionResult> PostLeaveApplication(int employeeId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay,bool medical, string alternateMobileNo)
        {
            try
            {
                await _leaveApplicationProvider.PostLeaveApplicationAsync(employeeId, leaveType, startDate, endDate, leaveReason, fromFirstHalf, fromSecondHalf, toFirstHalfDay, toSecondHalfDay, fromFullDay, medical ,alternateMobileNo);
                return Ok("Leave application submitted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetLeaveApplicationDetailsOfTheEmployeeByHRAdmin&SeniorManager/{employeeId}")]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> GetLeaveApplicationsAsync(int employeeId)
        {
            try
            {
                var leaveApplications = await _leaveApplicationProvider.GetLeaveApplicationsAsync(employeeId);
                return Ok(leaveApplications);
            }
            catch (InvalidOperationException ex)
            {
                // Log the exception if needed
                return BadRequest($"Error retrieving leave applications: {ex.Message}");
            }
        }

        [HttpPut("UpdateLeaveApplicationByLeaveApplicationId/{leaveApplicationId}")]
        public async Task<IActionResult> UpdateLeaveApplicationAsync(int leaveApplicationId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo)
        {
            try
            {
                await _leaveApplicationProvider.UpdateLeaveApplicationAsync(leaveApplicationId, leaveType, startDate, endDate, leaveReason, fromFirstHalf, fromSecondHalf,  toFirstHalfDay, toSecondHalfDay,  fromFullDay,  medical, alternateMobileNo);
                return Ok($"Leave application with Id {leaveApplicationId} updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error updating leave application: {ex.Message}");
            }
        }


        [HttpPut("UpdateLeaveApplicationStatusAsRejectionBySeniorManager/{employeeId}/{leaveApplicationId}/{leaveRejectionReason}")]
        public async Task<IActionResult> UpdateLeaveApplicationStatusAsRejectionBySeniorManager(int employeeId, int leaveApplicationId, string leaveRejectionReason)
        {
            try
            {

                await _leaveApplicationProvider.UpdateLeaveApplicationStatusAsRejectionBySeniorManager(employeeId, leaveApplicationId, leaveRejectionReason);

                // Sending email using SendGrid
                var client = new SendGridClient(_sendGridApiKey);
                var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
                var subject = "Leave Application Update";
                var to = new EmailAddress("recipient@example.com"); // Replace with recipient email address
                var plainTextContent = "Your Leave Status has been updated.";
                var htmlContent = "<strong>Your Leave Status has been updated.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);


                if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                          response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Sending email failed. Attendance update successful, but email not sent.");
                }

                return Ok("Leave status updated successfully. Email sent successfully.");
                //await _attendanceProvider.UpdateRegularizationStatusBySeniorManager(employeeId, attendanceId);
                //return Ok("Regularization status updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateLeaveApplicationStatusBySeniorManager/{employeeId}/{leaveApplicationId}")]
        public async Task<IActionResult> UpdateLeaveApplicationStatusBySeniorManager(int employeeId, int leaveApplicationId)
        {
            try
            {

                await _leaveApplicationProvider.UpdateLeaveApplicationStatusBySeniorManager(employeeId, leaveApplicationId);

                // Sending email using SendGrid
                var client = new SendGridClient(_sendGridApiKey);
                var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
                var subject = "Leave Application Update";
                var to = new EmailAddress("recipient@example.com"); // Replace with recipient email address
                var plainTextContent = "Your attendance has been updated.";
                var htmlContent = "<strong>Your attendance has been updated.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);


                if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                          response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Sending email failed. Leave update successful, but email not sent.");
                }

                return Ok("Leave Application status updated successfully. Email sent successfully.");
                //await _attendanceProvider.UpdateRegularizationStatusBySeniorManager(employeeId, attendanceId);
                //return Ok("Regularization status updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("ForViewingEmployeeLeaveApplication/{employeeId}")]
        public async Task<IActionResult> GetLeaveApplicationsOfEmployeeAsync(int employeeId)
        {
            try
            {
                var leaveApplications = await _leaveApplicationProvider.GetLeaveApplicationsOfEmployeeAsync(employeeId);
                return Ok(leaveApplications);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, log them, and return a meaningful response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
