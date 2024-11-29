using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class GenreRepository
{
    private readonly CinemaDbContext _dbContext;

    public GenreRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _dbContext.Genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(int id)
    {
        return await _dbContext.Genres.FirstOrDefaultAsync(g => g.GenreId == id);
    }

    public async Task AddGenreAsync(Genre genre)
    {
        await _dbContext.Genres.AddAsync(genre);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        _dbContext.Genres.Update(genre);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);
        if (genre != null)
        {
            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
        }
    }
}
