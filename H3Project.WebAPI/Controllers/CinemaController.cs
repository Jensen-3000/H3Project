using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemaController : ControllerBase
{
    private readonly CinemaRepository _cinemaRepository;

    public CinemaController(CinemaRepository cinemaRepository)
    {
        _cinemaRepository = cinemaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCinemas()
    {
        var cinemas = await _cinemaRepository.GetAllCinemasAsync();
        return Ok(cinemas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCinema(int id)
    {
        var cinema = await _cinemaRepository.GetCinemaByIdAsync(id);

        if (cinema == null)
        {
            return NotFound();
        }

        return Ok(cinema);
    }

    [HttpPost]
    public async Task<IActionResult> PostCinema(Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cinemaRepository.AddCinemaAsync(cinema);

        return CreatedAtAction(nameof(GetCinema), new { id = cinema.CinemaId }, cinema);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCinema(int id, Cinema cinema)
    {
        if (id != cinema.CinemaId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _cinemaRepository.UpdateCinemaAsync(cinema);
        }
        catch (Exception)
        {
            var exists = await _cinemaRepository.GetCinemaByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        await _cinemaRepository.DeleteCinemaAsync(id);
        return NoContent();
    }
}
