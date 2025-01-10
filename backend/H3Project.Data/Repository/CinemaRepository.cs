using H3Project.Data.Context;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class CinemaRepository : GenericRepository<CinemaModel>, ICinemaRepository
{
    public CinemaRepository(AppDbContext context) : base(context) { }

    public async Task<CinemaModel> GetCinemaWithScreensAsync(int id)
    {
        return await _context.Cinemas
            .Include(c => c.Screens)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
