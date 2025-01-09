using H3Project.Data.DTOs.Tickets;

namespace H3Project.Data.Services.Interfaces;

public interface ITicketService
{
    Task<List<TicketReadDto>> GetAllTicketsAsync();
    Task<TicketReadDto?> GetTicketByIdAsync(int id);
    Task<TicketReadDto> CreateTicketAsync(TicketCreateDto ticketCreateDto);
    Task<bool> UpdateTicketAsync(int id, TicketUpdateDto ticketUpdateDto);
    Task<bool> DeleteTicketAsync(int id);
}