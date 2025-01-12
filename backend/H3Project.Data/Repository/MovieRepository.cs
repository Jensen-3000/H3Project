using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class MovieRepository : GenericRepository<MovieModel>, IMovieRepository
{
    public MovieRepository(AppDbContext context) : base(context) { }

    public async Task<MovieModel?> GetMovieWithDetailsAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
            .Include(m => m.Screenings)
                .ThenInclude(s => s.Screen)
                    .ThenInclude(sc => sc.Cinema)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovieModel?> GetMovieBySlugAsync(string slug)
    {
        return await _context.Movies
            .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
            .Include(m => m.Screenings)
            .FirstOrDefaultAsync(m => m.Slug == slug);
    }

    public async Task UpdateMovieGenresAsync(MovieModel movie, List<int> genreIds)
    {
        var existingGenres = await _context.MovieGenres
            .Where(mg => mg.MovieId == movie.Id)
            .ToListAsync();

        _context.MovieGenres.RemoveRange(existingGenres);

        var newGenres = genreIds.Select(genreId => new MovieGenre
        {
            MovieId = movie.Id,
            GenreId = genreId
        });

        await _context.MovieGenres.AddRangeAsync(newGenres);
        await _context.SaveChangesAsync();
    }
}
