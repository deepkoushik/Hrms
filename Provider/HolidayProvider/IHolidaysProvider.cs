using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Providers
{
    public interface IHolidaysProvider
    {
        Task<List<Holiday>> GetHolidaysByYear(int year);
        Task<Holiday> PostTheHoldaysByTheHRAdmin(int employeeId, string name, DateTime date);
    }
}
