using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class GenreRepository : GenericRepository<GenreModel>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context) { }

    public async Task<GenreModel?> GetGenreWithMoviesAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.MovieGenres)
            .ThenInclude(mg => mg.Movie)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
}
