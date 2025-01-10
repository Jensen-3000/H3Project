using H3Project.Data.DTOs.Screenings;

namespace H3Project.Data.Services.Interfaces;

public interface IScreeningService
{
    Task<IEnumerable<ScreeningSimpleDto>> GetAllAsync();
    Task<ScreeningAvailableSeatsDto> GetByIdAsync(int id);
    Task<IEnumerable<ScreeningSimpleDto>> GetByMovieAsync(int movieId);
    Task<IEnumerable<ScreeningSimpleDto>> GetByDateRangeAsync(DateTime start, DateTime end);
    Task<ScreeningSimpleDto> CreateAsync(ScreeningCreateDto createDto);
    Task UpdateAsync(int id, ScreeningUpdateDto updateDto);
    Task DeleteAsync(int id);
}
