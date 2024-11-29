using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class BookingRepository
{
    private readonly CinemaDbContext _dbContext;

    public BookingRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task<Booking?> GetBookingByIdAsync(int id)
    {
        return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _dbContext.Bookings.AddAsync(booking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        _dbContext.Bookings.Update(booking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookingAsync(int id)
    {
        var booking = await _dbContext.Bookings.FindAsync(id);
        if (booking != null)
        {
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
        }
    }
}