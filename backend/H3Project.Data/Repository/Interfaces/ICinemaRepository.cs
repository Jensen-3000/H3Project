using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ICinemaRepository
{
    Task<IEnumerable<Cinema>> GetAllCinemasAsync();
    Task<Cinema> GetCinemaByIdAsync(int id);
    Task<IEnumerable<Cinema>> GetCinemasByMovieAsync(int movieId);
    Task AddCinemaAsync(Cinema cinema);
    Task UpdateCinemaAsync(Cinema cinema);
    Task DeleteCinemaAsync(int id);
}