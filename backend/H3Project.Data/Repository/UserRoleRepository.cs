using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class UserRoleRepository : GenericRepository<UserRoleModel>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }

    public async Task<UserRoleModel?> GetRoleWithUsersAsync(int id)
        => await _context.UserRoles
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<UserRoleModel?> GetRoleByNameAsync(string name)
        => await _context.UserRoles
            .FirstOrDefaultAsync(r => r.Role == name);
}
