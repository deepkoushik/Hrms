using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class ContactInformationRepository : IContactInformationRepository
    {
        private readonly KryptosHrmsDbContext _context;

        public ContactInformationRepository(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ContactInformation> GetAllContactInformation()
        {
            return _context.ContactInformations.ToList();
        }


        public void AddContactInformation(ContactInformation contactInfo)
        {
            _context.ContactInformations.Add(contactInfo);
            _context.SaveChanges();
        }


    }
}
