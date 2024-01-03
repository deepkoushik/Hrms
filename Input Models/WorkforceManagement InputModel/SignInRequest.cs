using Kryptos.Hrms.API.Models;
//using HRMS.Repository;
//using HRMSDB.Models;

namespace Kryptos.Hrms.API.Models
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string password { get; set; }
    }
}
