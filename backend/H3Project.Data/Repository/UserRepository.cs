using H3Project.Data.Context;
using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class UserRepository : GenericRepository<UserModel>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<UserModel?> GetUserWithDetails(int id)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Tickets)
                .ThenInclude(t => t.Screening)
                    .ThenInclude(s => s.Movie)
            .Include(u => u.Tickets)
                .ThenInclude(t => t.Screening)
                    .ThenInclude(s => s.Screen)
            .Include(t => t.Tickets)
                .ThenInclude(t => t.Seat)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserModel?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}