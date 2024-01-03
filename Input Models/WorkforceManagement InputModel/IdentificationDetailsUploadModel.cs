using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Models.Input_Model
{
    public class IdentificationDetailsUploadModel
    {
        public int EmployeeId { get; set; }
        public IFormFile PANAttachment { get; set; }
        public IFormFile AadharAttachment { get; set; }
        public IFormFile LicenseAttachment { get; set; }
      
    }

   
}
