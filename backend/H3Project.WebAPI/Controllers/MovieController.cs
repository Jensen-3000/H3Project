using H3Project.Data.DTOs.Movies;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetMovieBySlug(string slug)
    {
        var movie = await _movieService.GetMovieBySlugAsync(slug);
        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(MovieCreateDto movieCreateDto)
    {
        var newMovie = await _movieService.CreateMovieAsync(movieCreateDto);
        return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id }, newMovie);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
    {
        var result = await _movieService.UpdateMovieAsync(id, movieUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var result = await _movieService.DeleteMovieAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}