using Kryptos.Hrms.API;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Models.Input_Model;
using Kryptos.Hrms.API.Repositories;
using System.Collections.Generic;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Providers
{
    public class AcademicQualificationProvider : IAcademicQualificationProvider
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public AcademicQualificationProvider(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAcademicQualification(AcademicQualification academicQualification)
        {
            _dbContext.AcademicQualifications.Add(academicQualification);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
