using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/medicaldata")]
    [ApiController]
    public class MedicalDatumController : ControllerBase
    {
        private readonly IMedicalDatumProvider _medicalDatumProvider;

        public MedicalDatumController(IMedicalDatumProvider medicalDatumProvider)
        {
            _medicalDatumProvider = medicalDatumProvider;
        }

        // GET: api/medicaldata/employee/{employeeId}
        [HttpGet("employee/{employeeId}")]
        public ActionResult<List<MedicalDatum>> GetMedicalDataForEmployee(int employeeId)
        {
            var medicalData = _medicalDatumProvider.GetAllMedicalDataForEmployee(employeeId);
            return Ok(medicalData);
        }

        // POST: api/medicaldata
        [HttpPost]
        public ActionResult AddMedicalData([FromBody] MedicalDatum medicalData)
        {
            _medicalDatumProvider.AddMedicalData(medicalData);
            return CreatedAtAction(nameof(GetMedicalDataForEmployee), new { employeeId = medicalData.EmployeeId }, medicalData);
        }

        // PUT: api/medicaldata/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMedicalData(int id, [FromBody] MedicalDatum medicalData)
        {
            if (id != medicalData.Id)
            {
                return BadRequest();
            }

            _medicalDatumProvider.UpdateMedicalData(medicalData);
            return NoContent();
        }

        // DELETE: api/medicaldata/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicalData(int id)
        {
            _medicalDatumProvider.DeleteMedicalData(id);
            return NoContent();
        }
    }
}
