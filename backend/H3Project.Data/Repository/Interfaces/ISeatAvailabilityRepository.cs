using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ISeatAvailabilityRepository : IGenericRepository<SeatAvailabilityModel>
{
    Task<IEnumerable<SeatAvailabilityModel>> GetByScreeningAsync(int screeningId);
    Task<SeatAvailabilityModel?> GetByScreeningAndSeatAsync(int screeningId, int seatId);
    Task<bool> ToggleAvailabilityAsync(int screeningId, int seatId);
}
