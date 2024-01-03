using Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IEmployeeProvider
    {
        public SignInRequest UpdatePassword(SignInRequest request);
        Employee CreateEmployee(CreateEmployee employee,  double ctcpy);
        Employee GetEmployee(string id);
        List<Employee> GetAllEmployees();
        List<Employee> GetAllEmployeesByDepartmentOrManagerOrJob(int? departmentId, string? deptName, int? managerId, string? job);
        Employee UpdateEmployeeDetails(int id, UpdateByEmployee updatedEmployee, int updatedBy);
        Employee DeleteEmployee(int id);
        Job CreateJob(Job job);
        List<Job> GetAllJobs();
        List<Employee> GetActiveEmployees();
        List<Employee> GetInActiveEmployees();
        List<Employee> GetYourDepartmentEmployees(int userId);
       
    }
}
