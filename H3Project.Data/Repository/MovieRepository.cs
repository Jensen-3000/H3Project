using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class MovieRepository
{
    private readonly CinemaDbContext _dbContext;

    public MovieRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await _dbContext.Movies.ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _dbContext.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        _dbContext.Movies.Update(movie);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _dbContext.Movies.FindAsync(id);
        if (movie != null)
        {
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
