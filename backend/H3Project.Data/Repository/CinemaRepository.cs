using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class CinemaRepository : ICinemaRepository
{
    private readonly IAppDbContext _context;

    public CinemaRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
    {
        return await _context.Cinemas.AsNoTracking().ToListAsync();
    }

    public async Task<Cinema> GetCinemaByIdAsync(int id)
    {
        return await _context.Cinemas.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cinema>> GetCinemasByMovieAsync(int movieId)
    {
        return await _context.Schedules
            .Where(s => s.MovieId == movieId)
            .Select(s => s.Theater.Cinema)
            .Distinct()
            .ToListAsync();
    }

    public async Task AddCinemaAsync(Cinema cinema)
    {
        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCinemaAsync(Cinema cinema)
    {
        _context.Cinemas.Update(cinema);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCinemaAsync(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema != null)
        {
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
        }
    }
}