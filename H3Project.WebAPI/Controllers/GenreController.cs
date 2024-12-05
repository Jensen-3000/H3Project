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
    public async Task<IActionResult> GetGenres()
    {
        var genresDtos = await _context.Genres
            .AsNoTracking()
            .Select(g => new GenreDto(g.Id, g.Name))
            .ToListAsync();

        return Ok(genresDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genreDto = await _context.Genres
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new GenreDto(g.Id, g.Name))
            .FirstOrDefaultAsync();

        if (genreDto == null)
        {
            return NotFound();
        }

        return Ok(genreDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostGenre(GenreDto genreDto)
    {
        var genre = new Genre
        {
            Name = genreDto.Name
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        var newGenreDto = new GenreDto(genre.Id, genre.Name);

        return CreatedAtAction(nameof(GetGenre), new { id = newGenreDto.Id }, newGenreDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, GenreDto genreDto)
    {
        if (id != genreDto.Id)
        {
            return BadRequest();
        }

        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        genre.Name = genreDto.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
