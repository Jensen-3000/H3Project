using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemaController : ControllerBase
{
    private readonly ICinemaService _cinemaService;

    public CinemaController(ICinemaService cinemaService)
    {
        _cinemaService = cinemaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCinemas()
    {
        var cinemas = await _cinemaService.GetAllCinemasAsync();
        return Ok(cinemas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCinemaById(int id)
    {
        var cinema = await _cinemaService.GetCinemaByIdAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }
        return Ok(cinema);
    }

    [HttpGet("movie/{movieId:int}")]
    public async Task<IActionResult> GetCinemasByMovie(int movieId)
    {
        var cinemas = await _cinemaService.GetCinemasByMovieAsync(movieId);
        if (!cinemas.Any())
        {
            return NotFound($"No cinemas found showing movie with ID {movieId}");
        }
        return Ok(cinemas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCinema(CinemaCreateDto cinemaCreateDto)
    {
        var newCinema = await _cinemaService.CreateCinemaAsync(cinemaCreateDto);
        return CreatedAtAction(nameof(GetCinemaById), new { id = newCinema.Id }, newCinema);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCinema(int id, CinemaUpdateDto cinemaUpdateDto)
    {
        if (id != cinemaUpdateDto.Id)
        {
            return BadRequest();
        }

        await _cinemaService.UpdateCinemaAsync(cinemaUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        await _cinemaService.DeleteCinemaAsync(id);
        return NoContent();
    }
}
