using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Theaters;

namespace H3Project.Data.Services.Interfaces;

public interface ISeatService
{
    Task<List<SeatReadDto>> GetAllSeatsAsync();
    Task<SeatReadDto?> GetSeatByIdAsync(int id);
    Task<List<SeatReadDto>> GetSeatsByTheaterAsync(int theaterId);
    Task<TheaterLayoutDto?> GetTheaterLayoutAsync(int showtimeId);
    Task<bool> ReserveSeatsAsync(SeatReservationDto reservation, string username);
    Task<SeatReadDto> CreateSeatAsync(SeatCreateDto seatCreateDto);
    Task<bool> UpdateSeatAsync(int id, SeatUpdateDto seatUpdateDto);
    Task<bool> DeleteSeatAsync(int id);
}