using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Models.Input_Model;
using Kryptos.Hrms.API.Providers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicQualificationController : ControllerBase
    {
        private readonly IAcademicQualificationProvider _academicQualificationProvider;

        public AcademicQualificationController(IAcademicQualificationProvider academicQualificationProvider)
        {
            _academicQualificationProvider = academicQualificationProvider;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles(int employeeId,/*int QualificationId,int SpecializationId, int UniversityId, int InstituteId, int LanguageId , DateTime YearOfPassing, string CourseOfAppraisal, string CourseType, string Status, string CreatedBy, DateTime CreatedTime, string UpdatedBy , DateTime UpdateTime,*/ [FromForm] AcademicQualificationUploadModel model)
        {
            try
            {
                // Here you can validate the employeeId, check if the record exists, etc.

                // Convert IFormFile to byte array
                byte[] hsEMarsheetBytes = await GetBytesFromFormFile(model.HSEMarsheet);
                byte[] ugMarkSheetsBytes = await GetBytesFromFormFile(model.UGMarkSheets);
                byte[] UGProvisionalCertificate =await GetBytesFromFormFile(model.UGProvisionalCertificate);
                byte[] PGMarkSheets = await GetBytesFromFormFile(model.PGMarkSheets);
                byte[] PGProvisionalCertificate= await GetBytesFromFormFile(model.PGProvisionalCertificate);
                // Similarly, handle other files

                // Now you can save these byte arrays to your database or storage system
                // Example code (replace with your data access logic):
                var academicQualifications = new AcademicQualification
                {
                    EmployeeId = employeeId,
                    //QualificationId = QualificationId,
                    //SpecializationId = SpecializationId,
                    //UniversityId = UniversityId,
                    //InstituteId = InstituteId,
                    //LanguageId = LanguageId,
                    //YearOfPassing = YearOfPassing,
                    //CourseOfAppraisal = CourseOfAppraisal,
                    //CourseType = CourseType,
                    //Status = Status,
                    //CreatedBy = CreatedBy,
                    //CreatedTime= CreatedTime,
                    //UpdatedBy = UpdatedBy,
                    //UpdateTime=UpdateTime,
                    // Set other properties
                    HscmarkSheet = hsEMarsheetBytes,
                    UgmarkSheets = ugMarkSheetsBytes,
                    UgprovisionalCertificate = UGProvisionalCertificate,
                    PgmarkSheets = PGMarkSheets,
                    PgprovisionalCertificate = PGProvisionalCertificate,
                    // Set other byte[] properties;
                };

                _academicQualificationProvider.AddAcademicQualification(academicQualifications);
                _academicQualificationProvider.SaveChanges();

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
