using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ITicketRepository
{
    Task<List<Ticket>> GetTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(int id);
    Task AddTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(Ticket ticket);
    Task<Ticket> FindTicketByIdAsync(int id);
    bool TicketExists(int id);
}