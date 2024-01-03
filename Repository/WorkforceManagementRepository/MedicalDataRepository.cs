using System;
using System.Collections.Generic;
using System.Linq;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class MedicalDatumRepository : IMedicalDatumRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public MedicalDatumRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get a list of all medical data for a specific employee
        public List<MedicalDatum> GetAllMedicalDataForEmployee(int employeeId)
        {
            return _dbContext.MedicalData
                .Where(md => md.EmployeeId == employeeId)
                .ToList();
        }

        // Add a new medical data entry
        public void AddMedicalData(MedicalDatum medicalData)
        {
            _dbContext.MedicalData.Add(medicalData);
            _dbContext.SaveChanges();
        }

        // Update an existing medical data entry
        public void UpdateMedicalData(MedicalDatum medicalData)
        {
            _dbContext.MedicalData.Update(medicalData);
            _dbContext.SaveChanges();
        }

        // Delete a medical data entry
        public void DeleteMedicalData(int medicalDataId)
        {
            var medicalData = _dbContext.MedicalData.Find(medicalDataId);
            if (medicalData != null)
            {
                _dbContext.MedicalData.Remove(medicalData);
                _dbContext.SaveChanges();
            }
        }
    }
}
