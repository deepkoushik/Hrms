using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Repository.HolidayRepository
{
    public interface IHolidayRepository
    {
        Task<Holiday> PostTheHoldaysByTheHRAdmin(int empolyeeId, string name,DateTime date);
        Task<List<Holiday>> GetHolidaysByYear(int year);
    }
}
