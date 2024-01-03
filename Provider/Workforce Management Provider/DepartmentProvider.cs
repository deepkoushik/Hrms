using Kryptos.Hrms.API.Controller;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Provider
{

    public class DepartmentProvider : IDepartmentProvider
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentProvider(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public Department AddDepartment(Department department)
        {
            return _repository.AddDepartment(department);
        }



        public List<Department> GetAllDepartment()
        {
            return _repository.GetAllDepartment();
        }
        public List<Department> GetDepartment(int? deptId, string? deptName, string? location)
        {
            return _repository.GetDepartment(deptId, deptName, location);
        }



        public Department UpdateDepartment(UpdateDepartment department, int deptId)
        {
            return _repository.UpdateDepartment(department, deptId);
        }
    }
}



