using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

/// <summary>
/// Ticket Controller implementing REST API endpoints
/// </summary>
/// <remarks>
/// Documentation references:
/// - ASP.NET Core Web API Basics: https://learn.microsoft.com/da-dk/aspnet/core/tutorials/first-web-api
/// - Controller Actions Return Types: https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types
/// - Repository Pattern: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/advanced#implement-the-repository-and-unit-of-work-patterns
/// - REST API Best Practices: https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design
/// - Exception Handling: https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors
/// - Dependency Injection: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
/// - Async Programming: https://learn.microsoft.com/en-us/dotnet/csharp/async
/// - Controller Testing: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
/// </remarks>
[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly TicketRepository _ticketRepository;

    public TicketController(TicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);

        if (ticket == null)
            return NotFound();

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _ticketRepository.AddTicketAsync(ticket);

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(int id, Ticket ticket)
    {
        if (id != ticket.TicketId)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _ticketRepository.UpdateTicketAsync(ticket);
        }
        catch (Exception)
        {
            var exists = await _ticketRepository.GetTicketByIdAsync(id);
            if (exists == null)
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        if (ticket == null)
            return NotFound();

        await _ticketRepository.DeleteTicketAsync(id);
        return NoContent();
    }
}