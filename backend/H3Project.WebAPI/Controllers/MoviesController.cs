using H3Project.Data.DTOs.Movies;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieSimpleDto>>> GetAll()
        => Ok(await _movieService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDetailedDto>> GetById(int id)
        => Ok(await _movieService.GetByIdAsync(id));

    [HttpGet("{slug}")]
    public async Task<ActionResult<MovieDetailedDto>> GetBySlug(string slug)
        => Ok(await _movieService.GetBySlugAsync(slug));

    [HttpPost]
    public async Task<ActionResult<MovieSimpleDto>> Create(MovieCreateDto createDto)
    {
        var movie = await _movieService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MovieUpdateDto updateDto)
    {
        await _movieService.UpdateAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _movieService.DeleteAsync(id);
        return NoContent();
    }
}
