using H3Project.Data.Context;
using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class CinemaDetailRepository
{
    private readonly CinemaDbContext _dbContext;

    public CinemaDetailRepository(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CinemaDetail>> GetAllCinemaDetailsAsync()
    {
        return await _dbContext.CinemaDetails.ToListAsync();
    }

    public async Task<CinemaDetail?> GetCinemaDetailByIdAsync(int id)
    {
        return await _dbContext.CinemaDetails.FirstOrDefaultAsync(cd => cd.CinemaDetailId == id);
    }

    public async Task AddCinemaDetailAsync(CinemaDetail cinemaDetail)
    {
        await _dbContext.CinemaDetails.AddAsync(cinemaDetail);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCinemaDetailAsync(CinemaDetail cinemaDetail)
    {
        _dbContext.CinemaDetails.Update(cinemaDetail);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCinemaDetailAsync(int id)
    {
        var cinemaDetail = await _dbContext.CinemaDetails.FindAsync(id);
        if (cinemaDetail != null)
        {
            _dbContext.CinemaDetails.Remove(cinemaDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
