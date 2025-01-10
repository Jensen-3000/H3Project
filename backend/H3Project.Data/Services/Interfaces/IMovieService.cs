using H3Project.Data.DTOs.Movies;

namespace H3Project.Data.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieSimpleDto>> GetAllAsync();
    Task<MovieDetailedDto> GetByIdAsync(int id);
    Task<MovieDetailedDto> GetBySlugAsync(string slug);
    Task<MovieSimpleDto> CreateAsync(MovieCreateDto createDto);
    Task UpdateAsync(int id, MovieUpdateDto updateDto);
    Task DeleteAsync(int id);
}
