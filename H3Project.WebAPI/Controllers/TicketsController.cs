using AutoMapper;
using H3Project.Data.Context;
using H3Project.Data.DTOs.Tickets;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public TicketsController(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTickets()
    {
        var tickets = await _context.Tickets
            .Include(t => t.Schedule)
            .Include(t => t.Seat)
            .ToListAsync();

        return _mapper.Map<List<TicketReadDto>>(tickets);
    }

    // GET: api/Tickets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketReadDto>> GetTicket(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Schedule)
            .Include(t => t.Seat)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        return _mapper.Map<TicketReadDto>(ticket);
    }

    // PUT: api/Tickets/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(int id, TicketUpdateDto ticketDto)
    {
        if (id != ticketDto.Id)
        {
            return BadRequest();
        }

        var ticket = _mapper.Map<Ticket>(ticketDto);
        _context.Entry(ticket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TicketExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Tickets
    [HttpPost]
    public async Task<ActionResult<TicketReadDto>> PostTicket(TicketCreateDto ticketDto)
    {
        var ticket = _mapper.Map<Ticket>(ticketDto);
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var createdTicket = await _context.Tickets
            .Include(t => t.Schedule)
            .Include(t => t.Seat)
            .FirstOrDefaultAsync(t => t.Id == ticket.Id);

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, _mapper.Map<TicketReadDto>(createdTicket));
    }

    // DELETE: api/Tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TicketExists(int id)
    {
        return _context.Tickets.Any(e => e.Id == id);
    }
}
