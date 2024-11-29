using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class RoleRepository
{
    private readonly CinemaDbContext _dbContext;

    public RoleRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
    }

    public async Task AddRoleAsync(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRoleAsync(Role role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int id)
    {
        var role = await _dbContext.Roles.FindAsync(id);
        if (role != null)
        {
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}
