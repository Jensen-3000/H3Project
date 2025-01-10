using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;


public class SeatRepository : GenericRepository<SeatModel>, ISeatRepository
{
    public SeatRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<SeatModel>> GetSeatsByScreenAsync(int screenId)
    {
        return await _context.Seats
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Number)
            .ToListAsync();
    }

    public async Task<SeatModel?> GetSeatDetailsAsync(int id)
    {
        return await _context.Seats
            .Include(s => s.Screen)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
