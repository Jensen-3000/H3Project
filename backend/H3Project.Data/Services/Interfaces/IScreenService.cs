using H3Project.Data.DTOs.Screens;

namespace H3Project.Data.Services.Interfaces;

public interface IScreenService
{
    Task<IEnumerable<ScreenSimpleDto>> GetAllAsync();
    Task<ScreenDetailedDto> GetByIdAsync(int id);
    Task<IEnumerable<ScreenSimpleDto>> GetByCinemaAsync(int cinemaId);
    Task<ScreenSimpleDto> CreateAsync(ScreenCreateDto createDto);
    Task UpdateAsync(int id, ScreenUpdateDto updateDto);
    Task DeleteAsync(int id);
}
