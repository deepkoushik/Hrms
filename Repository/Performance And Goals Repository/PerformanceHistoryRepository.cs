using System;
using System.Collections.Generic;
using System.Linq;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repositories
{
    public class PerformanceHistoryRepository
    {
        private readonly KryptosHrmsDbContext _context;

        public PerformanceHistoryRepository(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        public PerformanceHistory GetById(int id)
        {
            return _context.PerformanceHistories.FirstOrDefault(ph => ph.Id == id);
        }

        public IEnumerable<PerformanceHistory> GetByEmployeeId(int employeeId)
        {
            return _context.PerformanceHistories
                .Where(ph => ph.EmployeeId == employeeId)
                .ToList();
        }

        public void Add(PerformanceHistory performanceHistory)
        {
            _context.PerformanceHistories.Add(performanceHistory);
            _context.SaveChanges();
        }

        public void Update(PerformanceHistory performanceHistory)
        {
            _context.PerformanceHistories.Update(performanceHistory);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var performanceHistory = GetById(id);
            if (performanceHistory != null)
            {
                _context.PerformanceHistories.Remove(performanceHistory);
                _context.SaveChanges();
            }
        }
    }
}
