using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly MovieRepository _movieRepository;

    public MovieController(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> PostMovie(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _movieRepository.AddMovieAsync(movie);

        return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {
        if (id != movie.MovieId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _movieRepository.UpdateMovieAsync(movie);
        }
        catch (Exception)
        {
            var exists = await _movieRepository.GetMovieByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        await _movieRepository.DeleteMovieAsync(id);
        return NoContent();
    }
}
