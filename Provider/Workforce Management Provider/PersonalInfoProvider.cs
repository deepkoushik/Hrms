using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PersonalInfoProvider : IPersonalInfoProvider
{
    private readonly IPersonalInfoRepository _repository;

    public PersonalInfoProvider(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PersonalInfo>> GetAllPersonalInfoAsync()
    {
        return await _repository.GetAllPersonalInfo();
    }

    public async Task<PersonalInfo> GetPersonalInfoByIdAsync(int id)
    {
        return await _repository.GetPersonalInfoById(id);
    }

    public async Task AddPersonalInfoAsync(PersonalInfo personalInfo)
    {
        await _repository.AddPersonalInfo(personalInfo);
    }

    public async Task UpdatePersonalInfoAsync(PersonalInfo personalInfo)
    {
        await _repository.UpdatePersonalInfo(personalInfo);
    }

    public async Task DeletePersonalInfoAsync(int id)
    {
        await _repository.DeletePersonalInfo(id);
    }
}
