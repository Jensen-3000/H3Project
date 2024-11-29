using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class SeatRepository
{
    private readonly CinemaDbContext _dbContext;

    public SeatRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Seat>> GetAllSeatsAsync()
    {
        return await _dbContext.Seats.ToListAsync();
    }

    public async Task<Seat?> GetSeatByIdAsync(int id)
    {
        return await _dbContext.Seats.FirstOrDefaultAsync(s => s.SeatId == id);
    }

    public async Task AddSeatAsync(Seat seat)
    {
        await _dbContext.Seats.AddAsync(seat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateSeatAsync(Seat seat)
    {
        _dbContext.Seats.Update(seat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSeatAsync(int id)
    {
        var seat = await _dbContext.Seats.FindAsync(id);
        if (seat != null)
        {
            _dbContext.Seats.Remove(seat);
            await _dbContext.SaveChangesAsync();
        }
    }
}
