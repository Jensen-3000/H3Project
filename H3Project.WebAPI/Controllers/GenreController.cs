using H3Project.Data.DTOs;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IRepository<Genre> _genreRepository;

    public GenreController(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _genreRepository.GetAllAsync();
        var genreDtos = genres.Select(g => new GenreDto(g.Id, g.Name));

        return Ok(genreDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var genreDto = new GenreDto(genre.Id, genre.Name);

        return Ok(genreDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostGenre(GenreDto genreDto)
    {
        var genre = new Genre
        {
            Name = genreDto.Name
        };

        await _genreRepository.AddAsync(genre);
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

        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        genre.Name = genreDto.Name;

        await _genreRepository.UpdateAsync(genre);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        await _genreRepository.DeleteAsync(id);

        return NoContent();
    }
}
