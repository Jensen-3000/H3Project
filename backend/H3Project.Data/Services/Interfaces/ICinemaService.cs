using H3Project.Data.DTOs.Cinemas;

namespace H3Project.Data.Services.Interfaces;

public interface ICinemaService
{
    Task<IEnumerable<CinemaReadDto>> GetAllCinemasAsync();
    Task<CinemaReadDto> GetCinemaByIdAsync(int id);
    Task<IEnumerable<CinemaReadDto>> GetCinemasByMovieAsync(int movieId);
    Task<CinemaReadDto> CreateCinemaAsync(CinemaCreateDto cinemaCreateDto);
    Task UpdateCinemaAsync(CinemaUpdateDto cinemaUpdateDto);
    Task DeleteCinemaAsync(int id);
}