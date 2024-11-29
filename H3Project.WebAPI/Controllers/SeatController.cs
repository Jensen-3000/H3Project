using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatController : ControllerBase
{
    private readonly SeatRepository _seatRepository;

    public SeatController(SeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetSeats()
    {
        var seats = await _seatRepository.GetAllSeatsAsync();
        return Ok(seats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSeat(int id)
    {
        var seat = await _seatRepository.GetSeatByIdAsync(id);

        if (seat == null)
        {
            return NotFound();
        }

        return Ok(seat);
    }

    [HttpPost]
    public async Task<IActionResult> PostSeat(Seat seat)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _seatRepository.AddSeatAsync(seat);

        return CreatedAtAction(nameof(GetSeat), new { id = seat.SeatId }, seat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeat(int id, Seat seat)
    {
        if (id != seat.SeatId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _seatRepository.UpdateSeatAsync(seat);
        }
        catch (Exception)
        {
            var exists = await _seatRepository.GetSeatByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeat(int id)
    {
        await _seatRepository.DeleteSeatAsync(id);
        return NoContent();
    }
}
