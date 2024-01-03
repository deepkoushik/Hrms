using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Azure;

[Route("api/[controller]/[action]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceProvider _attendanceProvider;
    private readonly string _sendGridApiKey = "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";
    public AttendanceController(IAttendanceProvider attendanceProvider)
    {
        _attendanceProvider = attendanceProvider;
        _sendGridApiKey = "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";
    }

    [HttpPost("checkin/{employeeId}")]
    public async Task<IActionResult> CheckIn(int employeeId)
    {
        try
        {
            Attendance attendance = await _attendanceProvider.CheckIn(employeeId);
            return Ok(attendance);
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

    [HttpPost("checkout/{employeeId}")]
    public async Task<IActionResult> CheckOut(int employeeId)
    {
        try
        {
            var attendance = await _attendanceProvider.CheckOut(employeeId);
            return Ok(attendance);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("attendanceDetailsByHRAdmin&SeniorManager/{employeeId}")]
    public IActionResult GetAttendanceDetails(int employeeId)
    {
        try
        {
            var attendanceDetails = _attendanceProvider.GetAttendanceDetails(employeeId);
            return Ok(attendanceDetails);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("attendanceDetailsOfParticularEmployee/{employeeId}")]
    public IActionResult GetAttendanceDetailsByEmployeeId(int employeeId)
    {
        try
        {
            var attendanceDetails = _attendanceProvider.GetAttendanceDetailsByEmployeeId(employeeId);
            return Ok(attendanceDetails);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("checkin-times/{employeeId}")]
    public IActionResult GetCheckInTimesOfTheCurrentDate(int employeeId)
    {
        var checkInTimes = _attendanceProvider.GetCheckInTimesOfTheCurrentDate(employeeId);

        if (checkInTimes.Count == 0)
        {
            return NotFound("No check-in times found for the specified employee on the current date.");
        }

        return Ok(checkInTimes);
    }


    [HttpPut("UpdateRegularizationByAttendance/{attendanceId}/{firstHalfAttendance}/{secondHalfAttendance}/{reasonForRegularization}")]
    public async Task<IActionResult> UpdateRegularizationByAttendanceId(int attendanceId, bool firstHalfAttendance, bool secondHalfAttendance, string reasonForRegularization)
    {
        try
        {
            await _attendanceProvider.UpdateRegularizationByAttendanceId(attendanceId, firstHalfAttendance, secondHalfAttendance, reasonForRegularization);
            return Ok("Regularization updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }





    [HttpGet("GetTheRegularizationListOftheEmployeesBasedOnTheSeniorManger&HRAdminId/{employeeId}")]
    public async Task<IActionResult> GetTheAttendanceListByEmployeeId(int employeeId)
    {
        try
        {
            List<Attendance> attendanceList = await _attendanceProvider.GetTheAttendanceListByEmployeeId(employeeId);
            return Ok(attendanceList);
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


    [HttpPut("UpdateRegularizationStatusBySeniorManager/{employeeId}/{attendanceId}")]
    public async Task<IActionResult> UpdateRegularizationStatusBySeniorManager(int employeeId, int attendanceId)
    {
        try
        {

            await _attendanceProvider.UpdateRegularizationStatusBySeniorManager(employeeId, attendanceId);

            // Sending email using SendGrid
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
            var subject = "Attendance Update";
            var to = new EmailAddress("recipient@example.com"); // Replace with recipient email address
            var plainTextContent = "Your attendance has been updated.";
            var htmlContent = "<strong>Your attendance has been updated.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);


            if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                      response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Sending email failed. Attendance update successful, but email not sent.");
            }

            return Ok("Regularization status updated successfully. Email sent successfully.");
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






    [HttpPut("UpdateRegularizationStatusAsRejectionBySeniorManager/{employeeId}/{attendanceId}/{lineManagerRejectionReason}")]
    public async Task<IActionResult> UpdateRegularizationStatusAsRejectionBySeniorManager(int employeeId, int attendanceId, string lineManagerRejectionReason)
    {
        try
        {

            await _attendanceProvider.UpdateRegularizationStatusAsRejectionBySeniorManager(employeeId, attendanceId, lineManagerRejectionReason);

            // Sending email using SendGrid
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
            var subject = "Attendance Update";
            var to = new EmailAddress("recipient@example.com"); // Replace with recipient email address
            var plainTextContent = "Your attendance has been updated.";
            var htmlContent = "<strong>Your attendance has been updated.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);


            if (response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                      response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Sending email failed. Attendance update successful, but email not sent.");
            }

            return Ok("Regularization status updated successfully. Email sent successfully.");
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
}




