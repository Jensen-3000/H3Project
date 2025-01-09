using H3Project.Data.DTOs.Theaters;

namespace H3Project.Data.Services.Interfaces;

public interface ITheaterService
{
    Task<List<TheaterReadDto>> GetAllTheatersAsync();
    Task<TheaterReadDto?> GetTheaterByIdAsync(int id);
    Task<TheaterReadDto> CreateTheaterAsync(TheaterCreateDto theaterCreateDto);
    Task<bool> UpdateTheaterAsync(int id, TheaterUpdateDto theaterUpdateDto);
    Task<bool> DeleteTheaterAsync(int id);
}