using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace SampleProject1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationProvider _contactInfoProvider;

        public ContactInformationController(IContactInformationProvider contactInfoProvider)
        {
            _contactInfoProvider = contactInfoProvider;
        }

        [HttpGet]
        public IActionResult GetAllContactInformation()
        {
            var contactInfoList = _contactInfoProvider.GetAllContactInformation();
            return Ok(contactInfoList);
        }

        [HttpPost]
        public IActionResult AddContactInformation(ContactInformation contactInfo)
        {
            _contactInfoProvider.AddContactInformation(contactInfo);
            return CreatedAtAction(nameof(GetAllContactInformation), null);
        }
    }
}
