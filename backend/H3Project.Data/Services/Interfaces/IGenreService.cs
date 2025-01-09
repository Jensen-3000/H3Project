using H3Project.Data.DTOs.Genres;

namespace H3Project.Data.Services.Interfaces;

public interface IGenreService
{
    Task<List<GenreReadDto>> GetAllGenresAsync();
    Task<GenreReadDto?> GetGenreByIdAsync(int id);
    Task<GenreReadDto> CreateGenreAsync(GenreCreateDto genreCreateDto);
    Task<bool> UpdateGenreAsync(int id, GenreUpdateDto genreUpdateDto);
    Task<bool> DeleteGenreAsync(int id);
}