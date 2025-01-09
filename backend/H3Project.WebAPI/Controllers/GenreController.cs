using H3Project.Data.DTOs.Genres;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(GenreCreateDto genreCreateDto)
    {
        var newGenre = await _genreService.CreateGenreAsync(genreCreateDto);
        return CreatedAtAction(nameof(GetGenreById), new { id = newGenre.Id }, newGenre);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreUpdateDto genreUpdateDto)
    {
        var result = await _genreService.UpdateGenreAsync(id, genreUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var result = await _genreService.DeleteGenreAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}