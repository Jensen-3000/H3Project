using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ICinemaRepository : IGenericRepository<CinemaModel>
{
    Task<CinemaModel> GetCinemaWithScreensAsync(int id);
}
