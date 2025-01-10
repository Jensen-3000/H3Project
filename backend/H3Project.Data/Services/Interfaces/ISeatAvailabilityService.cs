using H3Project.Data.DTOs.SeatAvailabilities;

namespace H3Project.Data.Services.Interfaces;

public interface ISeatAvailabilityService
{
    Task<IEnumerable<SeatAvailabilitySimpleDto>> GetByScreeningAsync(int screeningId);
    Task<SeatAvailabilityDetailedDto> GetByScreeningAndSeatAsync(int screeningId, int seatId);
    Task<bool> ToggleAvailabilityAsync(SeatAvailabilityToggleDto toggleDto);
}
