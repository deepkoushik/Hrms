using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IContactInformationProvider
    {
        IEnumerable<ContactInformation> GetAllContactInformation();
        void AddContactInformation(ContactInformation contactInfo);
    }
}
