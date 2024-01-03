using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IEmployeeProvider _provider;
        public JobController(IEmployeeProvider provider)
        {
            _provider = provider;
        }
        // Only HR Admin role can have access to create Job.
        //[Authorize(Roles = "HR Admin,Admin")]
        // It is checking the logged in user role and check the requirment role is matching or not.

    }
}
