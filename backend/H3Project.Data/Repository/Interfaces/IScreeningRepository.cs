using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IScreeningRepository : IGenericRepository<ScreeningModel>
{
    Task<ScreeningModel?> GetScreeningWithDetailsAsync(int id);
    Task<IEnumerable<ScreeningModel>> GetScreeningsByMovieAsync(int movieId);
    Task<IEnumerable<ScreeningModel>> GetScreeningsByDateRangeAsync(DateTime start, DateTime end);
}
