using Kryptos.Hrms.API.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IResignationProvider
    {
        Task<IEnumerable<Resignation>> GetAllResignationsAsync();
        Task<Resignation> GetResignationByIdAsync(int id);
        Task CreateResignationAsync(Resignation resignation);
        Task UpdateResignationAsync(Resignation resignation);
        Task DeleteResignationAsync(int id);
    }
}
