using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface ISeatRepository
{
    Task<List<Seat>> GetSeatsAsync();
    Task<Seat> GetSeatByIdAsync(int id);
    Task AddSeatAsync(Seat seat);
    Task UpdateSeatAsync(Seat seat);
    Task DeleteSeatAsync(Seat seat);
    Task<Seat> FindSeatByIdAsync(int id);
    bool SeatExists(int id);
}