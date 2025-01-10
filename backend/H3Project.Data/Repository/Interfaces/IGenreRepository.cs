using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IGenreRepository : IGenericRepository<GenreModel>
{
    Task<GenreModel?> GetGenreWithMoviesAsync(int id);
}
