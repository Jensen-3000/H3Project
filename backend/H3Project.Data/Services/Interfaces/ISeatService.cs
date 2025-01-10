using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.Services.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<SeatSimpleDto>> GetAllAsync();
    Task<SeatDetailedDto> GetByIdAsync(int id);
    Task<IEnumerable<SeatSimpleDto>> GetByScreenAsync(int screenId);
    Task<SeatSimpleDto> CreateAsync(SeatCreateDto createDto);
    Task UpdateAsync(int id, SeatUpdateDto updateDto);
    Task DeleteAsync(int id);
}
