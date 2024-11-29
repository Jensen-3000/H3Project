using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowtimeController : ControllerBase
{
    private readonly ShowtimeRepository _showtimeRepository;

    public ShowtimeController(ShowtimeRepository showtimeRepository)
    {
        _showtimeRepository = showtimeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetShowtimes()
    {
        var showtimes = await _showtimeRepository.GetAllShowtimesAsync();
        return Ok(showtimes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShowtime(int id)
    {
        var showtime = await _showtimeRepository.GetShowtimeByIdAsync(id);

        if (showtime == null)
        {
            return NotFound();
        }

        return Ok(showtime);
    }

    [HttpPost]
    public async Task<IActionResult> PostShowtime(Showtime showtime)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _showtimeRepository.AddShowtimeAsync(showtime);

        return CreatedAtAction(nameof(GetShowtime), new { id = showtime.ShowtimeId }, showtime);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShowtime(int id, Showtime showtime)
    {
        if (id != showtime.ShowtimeId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _showtimeRepository.UpdateShowtimeAsync(showtime);
        }
        catch (Exception)
        {
            var exists = await _showtimeRepository.GetShowtimeByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShowtime(int id)
    {
        await _showtimeRepository.DeleteShowtimeAsync(id);
        return NoContent();
    }
}
