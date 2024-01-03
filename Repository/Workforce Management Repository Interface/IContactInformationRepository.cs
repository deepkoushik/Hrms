using System;
using System.Collections.Generic;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API
{
    public interface IContactInformationRepository
    {
        IEnumerable<ContactInformation> GetAllContactInformation();

        void AddContactInformation(ContactInformation contactInfo);

    }
}
