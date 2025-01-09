using H3Project.Data.DTOs.Movies;

namespace H3Project.Data.Services.Interfaces;

public interface IMovieService
{
    Task<List<MovieReadDto>> GetAllMoviesAsync();
    Task<MovieReadDto?> GetMovieByIdAsync(int id);
    Task<MovieReadDto?> GetMovieBySlugAsync(string slug);
    Task<MovieReadDto> CreateMovieAsync(MovieCreateDto movieCreateDto);
    Task<bool> UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto);
    Task<bool> DeleteMovieAsync(int id);
}