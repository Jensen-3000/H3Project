using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class ScreenRepository : GenericRepository<ScreenModel>, IScreenRepository
{
    public ScreenRepository(AppDbContext context) : base(context) { }

    public async Task<ScreenModel> GetScreenWithDetailsAsync(int id)
    {
        return await _context.Screens
            .Include(s => s.Cinema)
            .Include(s => s.Seats)
            .Include(s => s.Screenings)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<ScreenModel>> GetScreensByCinemaAsync(int cinemaId)
    {
        return await _context.Screens
            .Where(s => s.CinemaId == cinemaId)
            .ToListAsync();
    }
}
