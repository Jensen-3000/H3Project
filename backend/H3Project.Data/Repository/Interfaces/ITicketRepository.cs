using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task AddTicketAsync(Ticket ticket);
    Task AddTicketsAsync(IEnumerable<Ticket> tickets);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(Ticket ticket);
}