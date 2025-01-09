using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Movie?> GetMovieBySlugAsync(string slug)
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Slug == slug);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }
}