using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;


public class BloodGroupRepository : IBloodGroupRepository
{
    private readonly KryptosHrmsDbContext _context;

    public BloodGroupRepository(KryptosHrmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<BloodGroup>> GetAllBloodGroupsAsync()
    {
        return await _context.BloodGroups.ToListAsync();
    }



    public async Task AddBloodGroupAsync(BloodGroup bloodGroup)
    {
        _context.BloodGroups.Add(bloodGroup);
        await _context.SaveChangesAsync();
    }



}
