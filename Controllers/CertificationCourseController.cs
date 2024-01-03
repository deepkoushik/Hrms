using Kryptos.Hrms.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace Kryptos.Hrms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificationCoursesController : ControllerBase
    {
        private readonly KryptosHrmsDbContext _context;

        public CertificationCoursesController(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCertificationCourse([FromForm] CertificationCourse certificationCourse, IFormFile certificationFile)
        {
            if (certificationFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await certificationFile.CopyToAsync(memoryStream);
                    certificationCourse.CertificationDocument = memoryStream.ToArray();
                }
            }

            _context.CertificationCourses.Add(certificationCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificationCourse", new { id = certificationCourse.Id }, certificationCourse);
        }

      
    }
}