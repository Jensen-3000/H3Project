using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IScreenRepository : IGenericRepository<ScreenModel>
{
    Task<ScreenModel> GetScreenWithDetailsAsync(int id);
    Task<IEnumerable<ScreenModel>> GetScreensByCinemaAsync(int cinemaId);
}