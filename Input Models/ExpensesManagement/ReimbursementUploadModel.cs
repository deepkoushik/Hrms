using System.Text.Json.Serialization;

namespace Kryptos.Hrms.API.Input_Models
{
    public class ReimbursementUploadModel
    {
        public int EmployeeId { get; set; }
        public string? ReferenceCode { get; set; }

        public string? BillNumber { get; set; }

        public DateTime? BillDate { get; set; }

        public string? BillPeriod { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        public int? ReimbutsementTypeId { get; set; }
        public IFormFile BillDocument { get; set; }
        public int? CreatedBy { get; set; }

    }
}
