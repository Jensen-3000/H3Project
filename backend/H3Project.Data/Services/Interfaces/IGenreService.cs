using H3Project.Data.DTOs.Genres;

namespace H3Project.Data.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreSimpleDto>> GetAllAsync();
    Task<GenreDetailedDto> GetByIdAsync(int id);
    Task<GenreSimpleDto> CreateAsync(GenreCreateDto createDto);
    Task UpdateAsync(int id, GenreUpdateDto updateDto);
    Task DeleteAsync(int id);
}
