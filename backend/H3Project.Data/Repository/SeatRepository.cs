using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace H3Project.Data.Repository;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Seat>> GetAllSeatsAsync()
    {
        return await _context.Seats.ToListAsync();
    }

    public async Task<Seat?> GetSeatByIdAsync(int id)
    {
        return await _context.Seats.FindAsync(id);
    }

    public async Task<List<Seat>> GetSeatsByTheaterAsync(int theaterId)
    {
        return await _context.Seats
            .Where(s => s.TheaterId == theaterId)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Number)
            .ToListAsync();
    }

    public async Task<List<Seat>> GetSeatsByShowtimeAsync(int showtimeId)
    {
        return await _context.Seats
            .Where(s => s.ScheduleId == showtimeId)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Number)
            .ToListAsync();
    }

    public async Task<List<int>> GetOccupiedSeatIdsAsync(int showtimeId)
    {
        return await _context.Tickets
            .Where(t => t.ScheduleId == showtimeId)
            .Select(t => t.SeatId)
            .ToListAsync();
    }

    public async Task AddSeatAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSeatAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSeatAsync(Seat seat)
    {
        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}