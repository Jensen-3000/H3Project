using H3Project.Data.DTOs;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IRepository<Movie> _movieRepository;

    public MovieController(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _movieRepository.GetAllAsync();
        var movieDtos = movies.Select(m => new MovieDto(m.Id, m.GenreId, m.Title, m.Description, m.ReleaseDate, m.Duration));

        return Ok(movieDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        var movieDto = new MovieDto(movie.Id, movie.GenreId, movie.Title, movie.Description, movie.ReleaseDate, movie.Duration);

        return Ok(movieDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostMovie(MovieDto movieDto)
    {
        var movie = new Movie
        {
            GenreId = movieDto.GenreId,
            Title = movieDto.Title,
            Description = movieDto.Description,
            ReleaseDate = movieDto.ReleaseDate,
            Duration = movieDto.Duration
        };

        await _movieRepository.AddAsync(movie);
        var newMovieDto = new MovieDto(movie.Id, movie.GenreId, movie.Title, movie.Description, movie.ReleaseDate, movie.Duration);

        return CreatedAtAction(nameof(GetMovie), new { id = newMovieDto.Id }, newMovieDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
    {
        if (id != movieDto.Id)
        {
            return BadRequest();
        }

        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        movie.GenreId = movieDto.GenreId;
        movie.Title = movieDto.Title;
        movie.Description = movieDto.Description;
        movie.ReleaseDate = movieDto.ReleaseDate;
        movie.Duration = movieDto.Duration;

        await _movieRepository.UpdateAsync(movie);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        await _movieRepository.DeleteAsync(id);

        return NoContent();
    }
}