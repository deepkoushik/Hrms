using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

public class AutoAttendanceJob : IJob
{
    private readonly IServiceProvider _provider;

    public AutoAttendanceJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            using (var scope = _provider.CreateScope())
            {
                // Resolve the AttendanceRepository
                var attendanceRepository = scope.ServiceProvider.GetRequiredService<AttendanceRepository>();

                // Get the current date and time
                DateTime currentTime = DateTime.Now;

                // Check if it's midnight (12:00 AM)
                if (currentTime.TimeOfDay == TimeSpan.Zero)
                {
                    // Get a list of employees who haven't posted attendance for today
                    var employeesWithoutAttendance = await attendanceRepository.GetEmployeesWithoutAttendanceAsync(currentTime.Date);

                    // Iterate through employees and post attendance for them
                    foreach (var employee in employeesWithoutAttendance)
                    {
                        // Create and post attendance with the required details
                        await attendanceRepository.PostAttendanceAutoAsync(employee.Id, currentTime.Date);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
