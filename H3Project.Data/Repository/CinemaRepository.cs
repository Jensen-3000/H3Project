using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class CinemaRepository
{
    private readonly CinemaDbContext _dbContext;

    public CinemaRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
    {
        return await _dbContext.Cinemas.Include(c => c.Screens).ThenInclude(s => s.Seats).ToListAsync();
    }

    public async Task<Cinema?> GetCinemaByIdAsync(int id)
    {
        return await _dbContext.Cinemas.Include(c => c.Screens).ThenInclude(s => s.Seats).FirstOrDefaultAsync(c => c.CinemaId == id);
    }

    public async Task AddCinemaAsync(Cinema cinema)
    {
        await _dbContext.Cinemas.AddAsync(cinema);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCinemaAsync(Cinema cinema)
    {
        _dbContext.Cinemas.Update(cinema);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCinemaAsync(int id)
    {
        var cinema = await _dbContext.Cinemas.FindAsync(id);
        if (cinema != null)
        {
            _dbContext.Cinemas.Remove(cinema);
            await _dbContext.SaveChangesAsync();
        }
    }
}
