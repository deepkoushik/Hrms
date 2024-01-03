
using Kryptos.Hrms.API.Models;



namespace Kryptos.Hrms.API.Repository
{
    public interface IDepartmentRepository
    {
        Department AddDepartment(Department department);
        Department UpdateDepartment(UpdateDepartment department, int deptId);
        List<Department> GetDepartment(int? deptId, string? deptName, string? location);
        List<Department> GetAllDepartment();


    }
}
