using H3Project.Data.Context;
using H3Project.Data.DTOs.Genres;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IAppDbContext _context;

    public GenreController(IAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genreModels = await _context.Genres
            .AsNoTracking()
            .ToListAsync();

        var genreDtos = genreModels.Select(MapModelToReadDto).ToList();

        return Ok(genreDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genreModel = await _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genreModel == null)
        {
            return NotFound();
        }

        var genreDto = MapModelToReadDto(genreModel);

        return Ok(genreDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(GenreCreateDto genreCreateDto)
    {
        var genreModel = MapCreateDtoToModel(genreCreateDto);

        _context.Genres.Add(genreModel);
        await _context.SaveChangesAsync();

        var newGenreDto = MapModelToReadDto(genreModel);

        return CreatedAtAction(nameof(GetGenreById), new { id = newGenreDto.Id }, newGenreDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreUpdateDto genreUpdateDto)
    {
        if (id != genreUpdateDto.Id)
        {
            return BadRequest();
        }

        var genreModel = await _context.Genres.FindAsync(id);
        if (genreModel == null)
        {
            return NotFound();
        }

        genreModel.Name = genreUpdateDto.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genreModel = await _context.Genres.FindAsync(id);
        if (genreModel == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genreModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static GenreReadDto MapModelToReadDto(Genre genre) => new(genre.Id, genre.Name);

    private static Genre MapCreateDtoToModel(GenreCreateDto genreCreateDto) => new() { Name = genreCreateDto.Name };
}
