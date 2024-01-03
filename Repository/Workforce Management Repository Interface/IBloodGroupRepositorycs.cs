
using Kryptos.Hrms.API.Models;


public interface IBloodGroupRepository
{
    Task<List<BloodGroup>> GetAllBloodGroupsAsync();

    Task AddBloodGroupAsync(BloodGroup bloodGroup);

}
