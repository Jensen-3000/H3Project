using H3Project.Data.DTOs.Genres;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreSimpleDto>>> GetAll()
    {
        return Ok(await _genreService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDetailedDto>> GetById(int id)
    {
        return Ok(await _genreService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<GenreSimpleDto>> Create(GenreCreateDto createDto)
    {
        var genre = await _genreService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GenreUpdateDto updateDto)
    {
        await _genreService.UpdateAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _genreService.DeleteAsync(id);
        return NoContent();
    }
}
