using H3Project.Data.DTOs.Tickets;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketSimpleDto>>> GetAll()
        => Ok(await _ticketService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketWithScreeningAndSeatDto>> GetById(int id)
        => Ok(await _ticketService.GetByIdAsync(id));

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TicketSimpleDto>>> GetByUser(int userId)
        => Ok(await _ticketService.GetByUserAsync(userId));

    [HttpGet("screening/{screeningId}")]
    public async Task<ActionResult<IEnumerable<TicketSimpleDto>>> GetByScreening(int screeningId)
        => Ok(await _ticketService.GetByScreeningAsync(screeningId));

    [HttpPost]
    public async Task<ActionResult<TicketSimpleDto>> Create(TicketCreateDto createDto)
    {
        var ticket = await _ticketService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ticketService.DeleteAsync(id);
        return NoContent();
    }
}
