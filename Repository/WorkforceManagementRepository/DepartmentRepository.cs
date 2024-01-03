using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository;

namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly KryptosHrmsDbContext _context;
    public DepartmentRepository(KryptosHrmsDbContext context)
    {
        _context = context;
    }
    public Department AddDepartment(Department department)
    {
        string spclChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
        var nums = "1234567890";
        var deptCheck = _context.Departments.FirstOrDefault(x => x.Name == department.Name);



        if (deptCheck == null)
        {
            if (!(department.Name.Any(c => spclChar.Contains(c)) || department.Name.Any(c => nums.Contains(c))))
            {

                _context.Add(department);
                _context.SaveChanges();
                return department;
            }


            else
            {
                throw new CustomException("Department name should not contain numbers or special characters.");
            }
        }
        else
        {
            throw new CustomException("Department name already exists.");
        }



    }



    public List<Department> GetAllDepartment()
    {
        try
        {
            return _context.Departments.ToList();
        }
        catch (Exception ex)
        {
            throw new CustomException("Department List is empty");
        }
    }



    public List<Department> GetDepartment(int? deptId, string? deptName, string? location)
    {
        try
        {
            if (deptId != null && deptId > 0)
            {
                List<Department> dept = _context.Departments.Where(x => x.Id == deptId).ToList();
                if (dept.Count > 0)
                {
                    return dept;
                }
                else
                {
                    throw new CustomException("Department list is empty");
                }
            }
            else if (!string.IsNullOrEmpty(deptName))
            {
                return _context.Departments.Where(x => x.Name.Contains(deptName)).ToList();
            }

            else
            {
                throw new CustomException("Input field is empty");
            }
        }
        catch (Exception ex)
        {
            throw new CustomException("Unable to get department details");
        }
    }



    public Department UpdateDepartment(UpdateDepartment department, int deptId)
    {
        try
        {
            var existingDepartment = _context.Departments.FirstOrDefault(x => x.Id == deptId);
            if (existingDepartment != null)
            {

                existingDepartment.Name = department.DepartmentName;
                _context.SaveChanges();
                return existingDepartment;
            }
            else
            {
                throw new CustomException("There is no record matching with department Id you gave");
            }
        }
        catch (Exception ex)
        {
            throw new CustomException("Error occured");
        }
    }
}
