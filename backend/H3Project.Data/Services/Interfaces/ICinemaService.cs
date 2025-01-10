using H3Project.Data.DTOs.Cinemas;

namespace H3Project.Data.Services.Interfaces;

public interface ICinemaService
{
    Task<IEnumerable<CinemaSimpleDto>> GetAllAsync();
    Task<CinemaDetailedDto> GetByIdAsync(int id);
    Task<CinemaSimpleDto> CreateAsync(CinemaCreateDto createDto);
    Task UpdateAsync(int id, CinemaUpdateDto updateDto);
    Task DeleteAsync(int id);
}
