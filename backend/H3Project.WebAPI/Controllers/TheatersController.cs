using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.Models;
using H3Project.Data.Services;
using H3Project.Data.Services.Interfaces;
using H3Project.Data.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheatersController : ControllerBase
{
    private readonly ITheaterService _theaterService;
    private readonly ISeatService _seatService;

    public TheatersController(ITheaterService theaterService, ISeatService seatService)
    {
        _theaterService = theaterService;
        _seatService = seatService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TheaterReadDto>>> GetTheaters()
    {
        var theaters = await _theaterService.GetAllTheatersAsync();
        return Ok(theaters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TheaterReadDto>> GetTheater(int id)
    {
        var theater = await _theaterService.GetTheaterByIdAsync(id);
        if (theater == null)
        {
            return NotFound();
        }

        return Ok(theater);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTheater(int id, TheaterUpdateDto theaterUpdateDto)
    {
        var result = await _theaterService.UpdateTheaterAsync(id, theaterUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TheaterReadDto>> PostTheater(TheaterCreateDto theaterCreateDto)
    {
        var newTheater = await _theaterService.CreateTheaterAsync(theaterCreateDto);
        return CreatedAtAction(nameof(GetTheater), new { id = newTheater.Id }, newTheater);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheater(int id)
    {
        var result = await _theaterService.DeleteTheaterAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/seats")]
    public async Task<ActionResult<IEnumerable<Seat>>> GenerateSeats(int id, [FromBody] SeatGenerationRequestDto request)
    {
        var seats = await _seatService.GenerateSeatsAsync(id, request);
        return Ok(seats);
    }
}