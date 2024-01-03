using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository.HolidayRepository;

namespace Kryptos.Hrms.API.Providers
{
    public class HolidaysProvider : IHolidaysProvider
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidaysProvider(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public async Task<List<Holiday>> GetHolidaysByYear(int year)
        {
            return await _holidayRepository.GetHolidaysByYear(year);
        }

        public async Task<Holiday> PostTheHoldaysByTheHRAdmin(int employeeId, string name, DateTime date)
        {
            return await _holidayRepository.PostTheHoldaysByTheHRAdmin(employeeId, name, date);
        }
    }
}
