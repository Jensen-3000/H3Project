using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly GenreRepository _genreRepository;

    public GenreController(GenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _genreRepository.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> PostGenre(Genre genre)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _genreRepository.AddGenreAsync(genre);

        return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, Genre genre)
    {
        if (id != genre.GenreId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _genreRepository.UpdateGenreAsync(genre);
        }
        catch (Exception)
        {
            var exists = await _genreRepository.GetGenreByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreRepository.DeleteGenreAsync(id);
        return NoContent();
    }
}
