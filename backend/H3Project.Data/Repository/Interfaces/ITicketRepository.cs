using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ITicketRepository : IGenericRepository<TicketModel>
{
    Task<TicketModel?> GetTicketWithDetailsAsync(int id);
    Task<IEnumerable<TicketModel>> GetTicketsByUserAsync(int userId);
    Task<IEnumerable<TicketModel>> GetTicketsByScreeningAsync(int screeningId);
}
