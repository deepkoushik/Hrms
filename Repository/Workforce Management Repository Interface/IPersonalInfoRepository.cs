using Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Repository
{
    public interface IPersonalInfoRepository
    {
        Task<PersonalInfo> GetPersonalInfoById(int id);
        Task<List<PersonalInfo>> GetAllPersonalInfo();
        Task AddPersonalInfo(PersonalInfo personalInfo);
        Task UpdatePersonalInfo(PersonalInfo personalInfo);
        Task DeletePersonalInfo(int id);
    }

}
