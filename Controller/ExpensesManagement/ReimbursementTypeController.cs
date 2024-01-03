using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.ExpensesManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kryptos.Hrms.API.Controller.Reimbursement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReimbursementTypeController : ControllerBase
    {
        private readonly IReimbursementTypePro _reimbursementTypeProvider;

        public ReimbursementTypeController(IReimbursementTypePro reimbursementTypeProvider)
        {
            _reimbursementTypeProvider = reimbursementTypeProvider;
        }

        [HttpGet("{id}")]
        public IActionResult GetReimbursementTypeById(int id)
        {
            var reimbursementType = _reimbursementTypeProvider.GetReimbursementTypeById(id);
            if (reimbursementType == null)
            {
                return NotFound(); // Return a 404 Not Found response if the resource doesn't exist.
            }

            return Ok(reimbursementType);
        }

        [HttpGet]
        public IActionResult GetAllReimbursementTypes()
        {
            var reimbursementTypes = _reimbursementTypeProvider.GetAllReimbursementTypes();
            return Ok(reimbursementTypes);
        }

        [HttpPost]
        public IActionResult AddReimbursementType(ReimbursementType reimbursementType)
        {
            _reimbursementTypeProvider.AddReimbursementType(reimbursementType);
            return CreatedAtAction(nameof(GetReimbursementTypeById), new { id = reimbursementType.Id }, reimbursementType);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReimbursementType(int id, ReimbursementType reimbursementType)
        {
            _reimbursementTypeProvider.UpdateReimbursementType(id, reimbursementType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReimbursementType(int id)
        {
            _reimbursementTypeProvider.DeleteReimbursementType(id);
            return NoContent();
        }
    }
}

