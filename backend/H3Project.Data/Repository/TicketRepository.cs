using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets
            .Include(t => t.Schedule)
            .Include(t => t.Seat)
            .ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Schedule)
            .Include(t => t.Seat)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task AddTicketsAsync(IEnumerable<Ticket> tickets)
    {
        await _context.Tickets.AddRangeAsync(tickets);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}