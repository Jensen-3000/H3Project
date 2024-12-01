using H3Project.Data.Context;
using H3Project.Data.DTOs.Movies;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IAppDbContext _context;

    public MovieController(IAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .ToListAsync();

        var moviesDtos = movies.Select(MapToMovieDto).ToList();

        return Ok(moviesDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        var movieDto = MapToMovieDto(movie);

        return Ok(movieDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostMovie(MovieCreateDto movieCreateDto)
    {
        var genres = await _context.Genres
            .Where(g => movieCreateDto.GenreIds.Contains(g.Id))
            .ToListAsync();

        var movie = new Movie
        {
            Title = movieCreateDto.Title,
            Description = movieCreateDto.Description,
            ReleaseDate = movieCreateDto.ReleaseDate,
            Duration = movieCreateDto.Duration,
            Genres = genres
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var newMovieDto = MapToMovieDto(movie);

        return CreatedAtAction(nameof(GetMovie), new { id = newMovieDto.Id }, newMovieDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieUpdateDto)
    {
        if (id != movieUpdateDto.Id)
        {
            return BadRequest();
        }

        var movie = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        var genres = await _context.Genres
            .Where(g => movieUpdateDto.GenreIds.Contains(g.Id))
            .ToListAsync();

        movie.Title = movieUpdateDto.Title;
        movie.Description = movieUpdateDto.Description;
        movie.ReleaseDate = movieUpdateDto.ReleaseDate;
        movie.Duration = movieUpdateDto.Duration;
        movie.Genres = genres;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static MovieDto MapToMovieDto(Movie movie)
    {
        return new MovieDto(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.ReleaseDate,
            movie.Duration,
            movie.Genres.Select(g => g.Name).ToList()
        );
    }
}
