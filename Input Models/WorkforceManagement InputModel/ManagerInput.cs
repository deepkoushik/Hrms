using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Models
{
    [Keyless]
    public class ManagerInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? ManagerId { get; set; }

    }
}
