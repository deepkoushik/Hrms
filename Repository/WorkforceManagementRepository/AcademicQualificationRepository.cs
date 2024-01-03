using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;

using System.Collections.Generic;


namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class AcademicQualificationRepository : IAcademicQualificationRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public AcademicQualificationRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(AcademicQualification academicQualification)
        {
            _dbContext.AcademicQualifications.Add(academicQualification);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}