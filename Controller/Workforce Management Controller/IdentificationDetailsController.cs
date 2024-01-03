using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models.Input_Model;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentificationDetailsController : ControllerBase
    {
        private readonly KryptosHrmsDbContext _dbContext; // Replace with your actual DbContext type

        public IdentificationDetailsController(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles(int employeeId, [FromForm] IdentificationDetailsUploadModel model)
        {
            try
            {
                // Here you can validate input, check if the employee exists, etc.

                // Convert IFormFile to byte arrays
                byte[] panAttachmentBytes = await GetBytesFromFormFile(model.PANAttachment);
                byte[] aadharAttachmentBytes = await GetBytesFromFormFile(model.AadharAttachment);
                byte[] licenseAttachmentBytes = await GetBytesFromFormFile(model.LicenseAttachment);

                // Similarly, handle other files

                // Now you can save these byte arrays to your database or storage system
                // Example code (replace with your data access logic):
                var identificationDetails = new IdentificationDetail
                {
                    EmployeeId = employeeId,
                    Panattachment = panAttachmentBytes,
                    AadharAttachment = aadharAttachmentBytes,
                    LicenseAttachment = licenseAttachmentBytes,

                    // Set other properties
                };

                _dbContext.IdentificationDetails.Add(identificationDetails);
                _dbContext.SaveChanges();

                return Ok("Files uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading files: " + ex.Message);
            }
        }

        private async Task<byte[]> GetBytesFromFormFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }


}
