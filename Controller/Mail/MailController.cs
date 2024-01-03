using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Controller.Mail
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
     
        private readonly string apiKey = "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";
        private readonly KryptosHrmsDbContext _dbContext;

        public MailController(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;


        }

        [HttpPost("SendEmails")]
        public async Task<IActionResult> SendEmails()
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("suhitha.s@kryptostech.com", "HR Team");
                var subject = "Regularization/Leave Reminder";

                var employeesToRemind = await _dbContext.Attendances
                    .Where(a => a.IsPresent == false)
                    .Select(a => new
                    {
                        a.Employee.EmailId,
                        a.Employee.FirstName,
                        a.CheckInTime,
                        a.CheckOutTime,
                    })
                    .Distinct()
                    .ToListAsync();

                foreach (var employee in employeesToRemind)
                {
                    var to = new EmailAddress(employee.EmailId, employee.FirstName);

                    // Compose the email content with dynamic table rows
                    var emailContent = $@"<html>
<head>
<style>
  table {{
    border-collapse: collapse;
    width: 100%;
  }}
  th, td {{
    border: 1px solid black;
    padding: 8px;
    text-align: center;
  }}
</style>
</head>
<body>
  <p>Hello</p>
  <p>{employee.FirstName},</p>
  <p>As per the attendance records, there are days for which you have not regularized your attendance. So, kindly apply regularization for attendance process or submit your leave application if applicable.</p>
  <table>
    <tr>
      <th>Roster Date</th>
      <th>Check In</th>
      <th>Check Out</th>
      <th>Reason for Regularization</th>
    </tr>
    <tr>
      <td>{employee.CheckInTime?.ToString("dd-MMM-yyyy")}</td>
      <td>{employee.CheckInTime?.ToString("HH:mm")}</td>
      <td>{employee.CheckOutTime?.ToString("HH:mm")}</td>
    </tr>
    <!-- Add more rows here as needed -->
  </table>
  <p>Please login to <a href=""http://localhost:3000/"">http://localhost:3000/</a> to view the details.</p>
  <p>Regards,<br>Team Kryptos</p>
  <p>Note: This is an automated email. Please do not respond.<br>The contents of this email are intended only for the use of the recipient.</p>
</body>
</html>";

                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent: emailContent);
                    var response = await client.SendEmailAsync(msg);
                }

                return Ok("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or perform appropriate error handling
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
