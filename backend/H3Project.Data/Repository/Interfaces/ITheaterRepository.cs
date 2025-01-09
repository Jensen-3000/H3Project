using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ITheaterRepository
{
    Task<List<Theater>> GetAllTheatersAsync();
    Task<Theater?> GetTheaterByIdAsync(int id);
    Task AddTheaterAsync(Theater theater);
    Task UpdateTheaterAsync(Theater theater);
    Task DeleteTheaterAsync(Theater theater);
}