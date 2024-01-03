using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPersonalInfoProvider
{
    Task<List<PersonalInfo>> GetAllPersonalInfoAsync();
    Task<PersonalInfo> GetPersonalInfoByIdAsync(int id);
    Task AddPersonalInfoAsync(PersonalInfo personalInfo);
    Task UpdatePersonalInfoAsync(PersonalInfo personalInfo);
    Task DeletePersonalInfoAsync(int id);
}
