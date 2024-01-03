using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.ExpensesManagement;
using Kryptos.Hrms.API.Repository.ExpenseManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Providers;
using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Repository;
using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Repository.WorkforceManagementRepository;
using Kryptos.Hrms.API;
using Kryptos.Hrms.API.Repository.CompensationAndBenefits;
using Kryptos.Hrms.API.Repository.CompensationAndBenefits_Interface;
using Kryptos.Hrms.API.Provider.InterfaceAttendance_LeaveProvider;
using Kryptos.Hrms.API.Provider.Attendance_LeaveClassProvider;
using Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository;
using Kryptos.Hrms.API.Repository.Attendance_LeaveClassRepository;
using Kryptos.Hrms.API.Repository.HolidayRepository;
using Kryptos.Hrms.API.Providers;
using Kryptos.Hrms.API.Repository.HolidayRepository;
using Kryptos.Hrms.API.Providers.InterfaceAttendance_LeaveProvider;
using Kryptos.Hrms.API.Providers.Attendance_LeaveClassProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KryptosHrmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<ILeaveProvider, LeaveProvider>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<IAttendanceProvider, AttendanceProviders>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<ILeaveApplicationRepository, LeaveApplicationRepository>();
builder.Services.AddScoped<ILeaveApplicationProvider, LeaveApplicationProvider>();
builder.Services.AddScoped<ILeaveAvailabilityRepository, LeaveAvailabilityRepository>();
builder.Services.AddScoped<ILeaveAvailabilityProvider, LeaveAvailabilityProvider>();
builder.Services.AddScoped<IReimbursementRepo, ReimbursementRepo>();
builder.Services.AddScoped<IReimbursementPro, ReimbursementPro>();
builder.Services.AddScoped<IReimbursementTypeRepo, ReimbursementTypeRepo>();
builder.Services.AddScoped<IReimbursementTypePro, ReimbursementTypePro>();
builder.Services.AddScoped<ISalaryDetailsRepo, SalaryDetailsRepo>();
builder.Services.AddScoped<ISalaryDetailsPro, SalaryDetailsPro>();
builder.Services.AddScoped<IHolidaysProvider, HolidaysProvider>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
builder.Services.AddScoped<IEarningsPercentageRepo, EarningsPercentageRepo>();
builder.Services.AddScoped<IEarningsPercentagePro, EarningsPercentagePro>();
builder.Services.AddScoped<IAchievementProvider, AchievementProvider>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IEmployeeProvider, EmployeeProvider>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeSalaryFunctionRepository>();
builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAcademicQualificationProvider, AcademicQualificationProvider>();
builder.Services.AddScoped<IAcademicQualificationRepository, AcademicQualificationRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IAchievementProvider, AchievementProvider>();
builder.Services.AddScoped<IBankInfoProvider, BankInfoProvider>();
builder.Services.AddScoped<IBankInfoRepository, BankInfoRepository>();
builder.Services.AddScoped<IBloodGroupProvider, BloodGroupProvider>();
builder.Services.AddScoped<IBloodGroupRepository, BloodGroupRepository>();
builder.Services.AddScoped<IContactInformationProvider, ContactInformationProvider>();
builder.Services.AddScoped<IContactInformationRepository, ContactInformationRepository>();
builder.Services.AddScoped<ICoreSkillProvider, CoreSkillProvider>();
builder.Services.AddScoped<ICoreSkillRepository, CoreSkillRepository>();
builder.Services.AddScoped<IMedicalDatumProvider, MedicalDatumProvider>();
builder.Services.AddScoped<IMedicalDatumRepository, MedicalDatumRepository>();
builder.Services.AddScoped<IPersonalInfoProvider, PersonalInfoProvider>();
builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
builder.Services.AddScoped<IReferenceProvider, ReferenceProvider>();
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();
builder.Services.AddScoped<IResignationProvider, ResignationProvider>();
builder.Services.AddScoped<IResignationRepository, ResignationRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddControllers();

// Configure Quartz.NET scheduler
var schedulerFactory = new StdSchedulerFactory();
var scheduler = schedulerFactory.GetScheduler().Result;

// Define a job and trigger to run daily at midnight
var jobDetail = JobBuilder.Create<AutoAttendanceJob>()
    .WithIdentity("AutoAttendanceJob", "AttendanceGroup")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("MidnightTrigger", "AttendanceGroup")
    .WithDailyTimeIntervalSchedule(s =>
        s.WithIntervalInHours(24) // Run every 24 hours (daily)
         .OnEveryDay()
         .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))) // Midnight
    .Build();

// Schedule the job with the trigger
scheduler.ScheduleJob(jobDetail, trigger).Wait();

// Start the Quartz.NET scheduler
scheduler.Start().Wait();

builder.Services.AddSingleton(scheduler); // Add Quartz.NET scheduler as a singleton

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy(name: "CorsPolicy",
    policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000") // Replace with the origin of your Next.js application
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
    
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
