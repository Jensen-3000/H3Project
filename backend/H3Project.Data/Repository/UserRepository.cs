using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserWithTicketsAsync(int id)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Schedule)
            .ThenInclude(s => s.Movie)
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Schedule)
            .ThenInclude(s => s.Theater)
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Seat)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(e => e.Id == id);
    }
}