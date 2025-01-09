using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;
    private readonly ILogger<SeatsController> _logger;

    public SeatsController(ISeatService seatService, ILogger<SeatsController> logger)
    {
        _seatService = seatService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeatReadDto>>> GetSeats()
    {
        var seats = await _seatService.GetAllSeatsAsync();
        return Ok(seats);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SeatReadDto>> GetSeat(int id)
    {
        var seat = await _seatService.GetSeatByIdAsync(id);
        if (seat == null)
        {
            return NotFound();
        }

        return Ok(seat);
    }

    [HttpGet("theater/{theaterId}")]
    public async Task<IActionResult> GetSeatsByTheater(int theaterId)
    {
        var seats = await _seatService.GetSeatsByTheaterAsync(theaterId);
        return Ok(seats);
    }

    [HttpGet("showtime/{showtimeId}/layout")]
    public async Task<ActionResult<TheaterLayoutDto>> GetTheaterLayout(int showtimeId)
    {
        var layout = await _seatService.GetTheaterLayoutAsync(showtimeId);
        if (layout == null)
        {
            return NotFound();
        }

        return Ok(layout);
    }

    [Authorize]
    [HttpPost("reserve")]
    public async Task<ActionResult<bool>> ReserveSeats([FromBody] SeatReservationDto reservation)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(username))
        {
            _logger.LogWarning("No username claim found in token");
            return Unauthorized();
        }

        var result = await _seatService.ReserveSeatsAsync(reservation, username);
        if (!result)
        {
            return BadRequest("Failed to reserve seats");
        }

        return Ok(true);
    }

    [HttpPost]
    public async Task<ActionResult<SeatReadDto>> PostSeat(SeatCreateDto seatCreateDto)
    {
        var newSeat = await _seatService.CreateSeatAsync(seatCreateDto);
        return CreatedAtAction(nameof(GetSeat), new { id = newSeat.Id }, newSeat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeat(int id, SeatUpdateDto seatUpdateDto)
    {
        var result = await _seatService.UpdateSeatAsync(id, seatUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeat(int id)
    {
        var result = await _seatService.DeleteSeatAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}