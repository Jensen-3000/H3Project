using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class TicketRepository : GenericRepository<TicketModel>, ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context) { }

    public async Task<TicketModel?> GetTicketWithDetailsAsync(int id)
        => await _context.Tickets
            .Include(t => t.User)
            .Include(t => t.Screening)
            .ThenInclude(s => s.Movie)
            .Include(t => t.Screening)
            .ThenInclude(s => s.Screen)
            .Include(t => t.Screening)
            .ThenInclude(s => s.SeatAvailabilities)
            .Include(t => t.Seat)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<TicketModel>> GetTicketsByUserAsync(int userId)
        => await _context.Tickets
            .Include(t => t.Screening)
            .ThenInclude(s => s.Movie)
            .Include(t => t.Screening)
            .ThenInclude(s => s.Screen)
            .Include(t => t.Screening)
            .ThenInclude(s => s.SeatAvailabilities)
            .Include(t => t.Seat)
            .Where(t => t.UserId == userId)
            .ToListAsync();

    public async Task<IEnumerable<TicketModel>> GetTicketsByScreeningAsync(int screeningId)
        => await _context.Tickets
            .Include(t => t.User)
            .Include(t => t.Seat)
            .Where(t => t.ScreeningId == screeningId)
            .ToListAsync();
}
