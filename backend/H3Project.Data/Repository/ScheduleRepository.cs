using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Schedule>> GetAllSchedulesAsync()
    {
        return await _context.Schedules
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ToListAsync();
    }

    public async Task<Schedule?> GetScheduleByIdAsync(int id)
    {
        return await _context.Schedules
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Schedule>> GetShowtimesAsync(int movieId, int cinemaId)
    {
        return await _context.Schedules
            .Where(s => s.MovieId == movieId && s.Theater.CinemaId == cinemaId)
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ToListAsync();
    }

    public async Task AddScheduleAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateScheduleAsync(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteScheduleAsync(Schedule schedule)
    {
        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
    }
}