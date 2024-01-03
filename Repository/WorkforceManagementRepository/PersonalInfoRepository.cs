using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;


namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public PersonalInfoRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonalInfo> GetPersonalInfoById(int id)
        {
            return await _dbContext.PersonalInfos.FindAsync(id);
        }

        public async Task<List<PersonalInfo>> GetAllPersonalInfo()
        {
            return await _dbContext.PersonalInfos.ToListAsync();
        }

        public async Task AddPersonalInfo(PersonalInfo personalInfo)
        {
            _dbContext.PersonalInfos.Add(personalInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePersonalInfo(PersonalInfo personalInfo)
        {
            _dbContext.Entry(personalInfo).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePersonalInfo(int id)
        {
            var personalInfo = await _dbContext.PersonalInfos.FindAsync(id);
            if (personalInfo != null)
            {
                _dbContext.PersonalInfos.Remove(personalInfo);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
