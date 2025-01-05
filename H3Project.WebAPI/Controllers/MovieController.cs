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
    public async Task<IActionResult> GetAllMovies()
    {
        var movieModels = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .ToListAsync();

        var movieDtos = movieModels.Select(MapModelToReadDto).ToList();

        return Ok(movieDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movieModel = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movieModel == null)
        {
            return NotFound();
        }

        var movieDto = MapModelToReadDto(movieModel);

        return Ok(movieDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(MovieCreateDto movieCreateDto)
    {
        var genres = await _context.Genres
            .Where(g => movieCreateDto.GenreIds.Contains(g.Id))
            .ToListAsync();

        var movieModel = MapCreateDtoToModel(movieCreateDto, genres);

        _context.Movies.Add(movieModel);
        await _context.SaveChangesAsync();

        var newMovieDto = MapModelToReadDto(movieModel);

        return CreatedAtAction(nameof(GetMovieById), new { id = newMovieDto.Id }, newMovieDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
    {
        if (id != movieUpdateDto.Id)
        {
            return BadRequest();
        }

        var movieModel = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movieModel == null)
        {
            return NotFound();
        }

        var genres = await _context.Genres
            .Where(g => movieUpdateDto.GenreIds.Contains(g.Id))
            .ToListAsync();

        MapUpdateDtoToModel(movieUpdateDto, movieModel, genres);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movieModel = await _context.Movies.FindAsync(id);
        if (movieModel == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movieModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static MovieReadDto MapModelToReadDto(Movie movie) => new(
        movie.Id,
        movie.Title,
        movie.Description,
        movie.ReleaseDate,
        movie.Duration,
        movie.Genres.Select(g => g.Name).ToList()
    );

    private static Movie MapCreateDtoToModel(MovieCreateDto movieCreateDto, List<Genre> genres) => new()
    {
        Title = movieCreateDto.Title,
        Description = movieCreateDto.Description,
        ReleaseDate = movieCreateDto.ReleaseDate,
        Duration = movieCreateDto.Duration,
        Genres = genres
    };

    private static void MapUpdateDtoToModel(MovieUpdateDto movieUpdateDto, Movie movie, List<Genre> genres)
    {
        movie.Title = movieUpdateDto.Title;
        movie.Description = movieUpdateDto.Description;
        movie.ReleaseDate = movieUpdateDto.ReleaseDate;
        movie.Duration = movieUpdateDto.Duration;
        movie.Genres = genres;
    }
}
