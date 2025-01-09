using H3Project.Data.Context;
using H3Project.Data.DTOs.Movies;
using H3Project.Data.DTOs.Schedules;
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
    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.Genres)
            .Select(m => new MovieReadDto
            {
                Id = m.Id,
                Title = m.Title,
                Slug = m.Slug,
                Description = m.Description,
                ImageUrl = m.ImageUrl,
                ReleaseDate = m.ReleaseDate,
                Duration = m.Duration,
                Genres = m.Genres.Select(g => g.Name).ToList()
            })
            .ToListAsync();

        return Ok(movies);
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

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetMovieBySlug(string slug)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Slug == slug);

        if (movie == null)
            return NotFound();

        return Ok(MapModelToReadDto(movie));
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

    private static MovieReadDto MapModelToReadDto(Movie movie)
    {
        return new MovieReadDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Slug = movie.Slug,
            Description = movie.Description,
            ImageUrl = movie.ImageUrl,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            Genres = movie.Genres.Select(g => g.Name).ToList(),
            CurrentSchedules = movie.Schedules
                .Select(s => new ScheduleReadDto
                {
                    Id = s.Id,
                    ShowTime = s.ShowTime,
                    EndTime = s.EndTime,
                    BasePrice = s.BasePrice,
                    TheaterId = s.TheaterId,
                    TheaterName = s.Theater.Name,
                    MovieId = s.MovieId,
                    MovieTitle = s.Movie.Title
                }).ToList()
        };
    }

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
