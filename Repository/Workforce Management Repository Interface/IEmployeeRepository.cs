using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider;





namespace Kryptos.Hrms.API.Repository
{
    public interface IEmployeeRepository
    {
        public SignInRequest UpdatePassword(SignInRequest request);
        Employee CreateEmployee(CreateEmployee employee, double ctcpy);
        Employee GetEmployee(string id);
        List<Employee> GetAllEmployees();
        List<Employee> GetAllEmployeesByDepartmentOrManagerOrJob(int? departmentId, string? deptName, int? managerId, string? job);
        Employee UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee, int updatedBy);
        Employee DeleteEmployee(int id);
        public Job CreateJob(Job job);
        List<Job> GetAllJobs();
        List<Employee> GetActiveEmployees();
        List<Employee> GetInActiveEmployees();
        List<Employee> GetYourDepartmentEmployees(int userId);



        void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt);



    }
}