using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class SeatAvailabilityRepository : GenericRepository<SeatAvailabilityModel>, ISeatAvailabilityRepository
{
    public SeatAvailabilityRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<SeatAvailabilityModel>> GetByScreeningAsync(int screeningId)
    {
        return await _context.SeatAvailabilities
            .Include(sa => sa.Seat)
            .Where(sa => sa.ScreeningId == screeningId)
            .ToListAsync();
    }

    public async Task<SeatAvailabilityModel?> GetByScreeningAndSeatAsync(int screeningId, int seatId)
    {
        return await _context.SeatAvailabilities
            .Include(sa => sa.Seat)
            .Include(sa => sa.Screening)
                .ThenInclude(s => s.Movie)
            .Include(sa => sa.Screening)
                .ThenInclude(s => s.Screen)
            .FirstOrDefaultAsync(sa => sa.ScreeningId == screeningId && sa.SeatId == seatId);
    }

    public async Task<bool> ToggleAvailabilityAsync(int screeningId, int seatId)
    {
        var availability = await GetByScreeningAndSeatAsync(screeningId, seatId);
        if (availability == null)
        {
            return false;
        }

        availability.IsAvailable = !availability.IsAvailable;
        await _context.SaveChangesAsync();

        return availability.IsAvailable;
    }
}
