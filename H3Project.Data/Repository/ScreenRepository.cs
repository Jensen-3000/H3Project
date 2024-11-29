using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class ScreenRepository
{
    private readonly CinemaDbContext _dbContext;

    public ScreenRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Screen>> GetAllScreensAsync()
    {
        return await _dbContext.Screens.ToListAsync();
    }

    public async Task<Screen?> GetScreenByIdAsync(int id)
    {
        return await _dbContext.Screens.FirstOrDefaultAsync(s => s.ScreenId == id);
    }

    public async Task AddScreenAsync(Screen screen)
    {
        await _dbContext.Screens.AddAsync(screen);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateScreenAsync(Screen screen)
    {
        _dbContext.Screens.Update(screen);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteScreenAsync(int id)
    {
        var screen = await _dbContext.Screens.FindAsync(id);
        if (screen != null)
        {
            _dbContext.Screens.Remove(screen);
            await _dbContext.SaveChangesAsync();
        }
    }
}
