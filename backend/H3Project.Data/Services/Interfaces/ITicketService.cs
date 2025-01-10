using H3Project.Data.DTOs.Tickets;

namespace H3Project.Data.Services.Interfaces;

public interface ITicketService
{
    Task<IEnumerable<TicketSimpleDto>> GetAllAsync();
    Task<TicketWithScreeningAndSeatDto> GetByIdAsync(int id);
    Task<IEnumerable<TicketSimpleDto>> GetByUserAsync(int userId);
    Task<IEnumerable<TicketSimpleDto>> GetByScreeningAsync(int screeningId);
    Task<TicketSimpleDto> CreateAsync(TicketCreateDto createDto);
    Task DeleteAsync(int id);
}
