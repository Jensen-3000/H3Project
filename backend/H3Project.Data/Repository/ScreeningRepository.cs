using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class ScreeningRepository : GenericRepository<ScreeningModel>, IScreeningRepository
{
    public ScreeningRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<ScreeningModel?> GetScreeningWithDetailsAsync(int id)
    {
        return await _context.Screenings
            .Include(s => s.Screen)
            .Include(s => s.Movie)
            .Include(s => s.SeatAvailabilities)
            .ThenInclude(sa => sa.Seat)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<ScreeningModel>> GetScreeningsByMovieAsync(int movieId)
    {
        return await _context.Screenings
            .Include(s => s.Screen)
            .Where(s => s.MovieId == movieId && s.StartTime > DateTime.Now)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<ScreeningModel>> GetScreeningsByDateRangeAsync(DateTime start, DateTime end)
    {
        return await _context.Screenings
            .Include(s => s.Screen)
            .Include(s => s.Movie)
            .Where(s => s.StartTime >= start && s.StartTime <= end)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }
}
