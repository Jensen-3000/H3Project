using H3Project.Data.DTOs.Tickets;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketReadDto>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(int id, TicketUpdateDto ticketUpdateDto)
    {
        var result = await _ticketService.UpdateTicketAsync(id, ticketUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TicketReadDto>> PostTicket(TicketCreateDto ticketCreateDto)
    {
        var newTicket = await _ticketService.CreateTicketAsync(ticketCreateDto);
        return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var result = await _ticketService.DeleteTicketAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}