using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBloodGroupProvider
{
    Task<List<BloodGroup>> GetAllBloodGroupsAsync();
    Task AddBloodGroupAsync(BloodGroup bloodGroup);
}
