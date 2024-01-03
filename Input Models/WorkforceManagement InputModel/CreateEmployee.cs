using System.Security.Policy;

namespace Kryptos.Hrms.API.Models
{
    public class CreateEmployee
    {
        //public int CreatedByID { get; set; }
        public string FirstName { get; set; } = null!;



        public string? LastName { get; set; }



        public string EmailId { get; set; } = null!;



        public string Password { get; set; } = null!;



        public String? PhoneNumber { get; set; }



       

        public DateTime HireDate { get; set; }



        public int? JobId { get; set; }



        public int? DepartmentId { get; set; }



        public int? ManagerId { get; set; }
        public string Roles { get; set; }



    }
}


