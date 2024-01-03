using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kryptos.Hrms.API.Controller.Login
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private ILog _logger;
        private readonly IEmployeeProvider _provider;
        private readonly KryptosHrmsDbContext _context;
        private readonly IEmployeeRepository _repo;
        public LoginController(KryptosHrmsDbContext context, IEmployeeProvider provider, IEmployeeRepository repository)
        {
            _repo = repository;
            //_logger= logger;
            _context = context;
            _provider = provider;
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            var user = _context.Employees.FirstOrDefault(x => x.EmailId == request.Email && x.IsActive == true);
            // Checking the email and password is matching with the DB.
            if (user == null)
            {
                return NotFound("User not found");
            }
            var decryptedPassword = _repo.VerifyPasswordhash(request.password, user.PasswordHash, user.PasswordSalt);
            if (!decryptedPassword)
            //if (user is null || user.IsActive == false)
            {
                return BadRequest("Invalid credentials.");
            }
            // Creating claims to check the Id and roles in authorization.
            var claims = new List<Claim>
            {
                 new Claim(type: ClaimTypes.Email, value: request.Email),
                 new Claim(type: ClaimTypes.Name,value: user.FirstName),
                 new Claim(type: ClaimTypes.NameIdentifier,value: user.Id.ToString()),
                 new Claim(type: ClaimTypes.Role,value: user.Role),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            });
            //_logger.Information($"{user.FirstName} signed in");
            Response.Cookies.Append("UserRole", user.Role);
            Response.Cookies.Append("UserId", user.Id.ToString());
            Response.Cookies.Append("UserName", user.FirstName + " " + user.LastName);
            Response.Cookies.Append("DepartmentID", user.DepartmentId.ToString());
            return Ok(new
            {
                Message = "Signed In Succesfully",
                Role = user.Role,
                EmployeeID = user.Id,
                EmployeeName = user.FirstName + " " + user.LastName,
                DepartmentID = user.DepartmentId,

            });
        }
        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // _logger.Information($"{User.FindFirstValue(ClaimTypes.Name)} signed out succesfully");
            return Ok("Signed out successfully");
        }
    }

}
