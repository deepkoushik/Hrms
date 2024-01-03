using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BloodGroupProvider : IBloodGroupProvider
{
    private readonly IBloodGroupRepository _repository;

    public BloodGroupProvider(IBloodGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BloodGroup>> GetAllBloodGroupsAsync()
    {
        return await _repository.GetAllBloodGroupsAsync();
    }

    public async Task AddBloodGroupAsync(BloodGroup bloodGroup)
    {
        await _repository.AddBloodGroupAsync(bloodGroup);
    }
}
