using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API;
using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Providers
{
    public class ContactInformationProvider : IContactInformationProvider
    {
        private readonly IContactInformationRepository _repository;

        public ContactInformationProvider(IContactInformationRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ContactInformation> GetAllContactInformation()
        {
            return _repository.GetAllContactInformation();
        }

        public void AddContactInformation(ContactInformation contactInfo)
        {
            _repository.AddContactInformation(contactInfo);
        }
    }
}
