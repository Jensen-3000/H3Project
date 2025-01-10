using H3Project.Data.DTOs.Seats;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatsController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeatSimpleDto>>> GetAll()
    {
        return Ok(await _seatService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SeatDetailedDto>> GetById(int id)
    {
        return Ok(await _seatService.GetByIdAsync(id));
    }

    [HttpGet("screen/{screenId}")]
    public async Task<ActionResult<IEnumerable<SeatSimpleDto>>> GetByScreen(int screenId)
    {
        return Ok(await _seatService.GetByScreenAsync(screenId));
    }

    [HttpPost]
    public async Task<ActionResult<SeatSimpleDto>> Create(SeatCreateDto createDto)
    {
        var seat = await _seatService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = seat.Id }, seat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SeatUpdateDto updateDto)
    {
        await _seatService.UpdateAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _seatService.DeleteAsync(id);
        return NoContent();
    }
}