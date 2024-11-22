using H3Project.Data.Context;
using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class TicketRepository
{
    private readonly CinemaDbContext _dbContext;

    public TicketRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _dbContext.Tickets.ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await _dbContext.Tickets.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _dbContext.Tickets.Update(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(int id)
    {
        var ticket = await _dbContext.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
        }
    }
}