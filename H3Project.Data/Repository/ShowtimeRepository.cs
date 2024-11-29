using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class ShowtimeRepository
{
    private readonly CinemaDbContext _dbContext;

    public ShowtimeRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Showtime>> GetAllShowtimesAsync()
    {
        return await _dbContext.Showtimes.ToListAsync();
    }

    public async Task<Showtime?> GetShowtimeByIdAsync(int id)
    {
        return await _dbContext.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == id);
    }

    public async Task AddShowtimeAsync(Showtime showtime)
    {
        await _dbContext.Showtimes.AddAsync(showtime);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateShowtimeAsync(Showtime showtime)
    {
        _dbContext.Showtimes.Update(showtime);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteShowtimeAsync(int id)
    {
        var showtime = await _dbContext.Showtimes.FindAsync(id);
        if (showtime != null)
        {
            _dbContext.Showtimes.Remove(showtime);
            await _dbContext.SaveChangesAsync();
        }
    }
}
