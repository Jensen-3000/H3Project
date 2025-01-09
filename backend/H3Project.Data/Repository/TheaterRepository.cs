using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class TheaterRepository : ITheaterRepository
{
    private readonly AppDbContext _context;

    public TheaterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Theater>> GetAllTheatersAsync()
    {
        return await _context.Theaters.ToListAsync();
    }

    public async Task<Theater?> GetTheaterByIdAsync(int id)
    {
        return await _context.Theaters.FindAsync(id);
    }

    public async Task AddTheaterAsync(Theater theater)
    {
        await _context.Theaters.AddAsync(theater);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTheaterAsync(Theater theater)
    {
        _context.Theaters.Update(theater);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTheaterAsync(Theater theater)
    {
        _context.Theaters.Remove(theater);
        await _context.SaveChangesAsync();
    }
}