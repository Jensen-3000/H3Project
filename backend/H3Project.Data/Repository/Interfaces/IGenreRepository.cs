using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreByIdAsync(int id);
    Task AddGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(Genre genre);
}