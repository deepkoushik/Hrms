using Kryptos.Hrms.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Repository.HolidayRepository;

public class HolidayRepository : IHolidayRepository
{
    private readonly KryptosHrmsDbContext _dbContext;

    public HolidayRepository(KryptosHrmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Holiday> PostTheHoldaysByTheHRAdmin(int employeeId, string name, DateTime date)
    {
        // Check if the employee with the given ID has the role "HR Admin"
        var isAdmin = await _dbContext.Employees
                                    .AnyAsync(e => e.Id == employeeId && e.Role == "HR Admin");

        if (!isAdmin)
        {
            throw new UnauthorizedAccessException("Only HR Admin can post holidays.");
        }

        // Format the date as "Date/Month/Year"
        var formattedDate = date.ToString("dd/MM/yyyy");

        // Parse the formatted date back to DateTime object
        if (DateTime.TryParseExact(formattedDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            // Create a new Holiday object with the provided data
            var holiday = new Holiday
            {
                EmployeeId = employeeId,
                Name = name,
                Date = parsedDate,
                // Set createdBy to employeeId and createdDateTime to current date time
                CreatedBy = employeeId,
                CreatedDateTime = DateTime.Now
            };

            // Add the holiday to the database context and save changes
            _dbContext.Holidays.Add(holiday);
            await _dbContext.SaveChangesAsync();

            return holiday;
        }
        else
        {
            throw new FormatException("Invalid date format.");
        }
    }

    public async Task<List<Holiday>> GetHolidaysByYear(int year)
    {
        var holidaysInYear = await _dbContext.Holidays
                                            .Where(h => h.Date.HasValue && h.Date.Value.Year == year)
                                            .ToListAsync();

        return holidaysInYear;
    }



}
