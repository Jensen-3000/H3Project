using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace H3Project.Data.Repository.Interfaces;

public interface ISeatRepository
{
    Task<List<Seat>> GetAllSeatsAsync();
    Task<Seat?> GetSeatByIdAsync(int id);
    Task<List<Seat>> GetSeatsByTheaterAsync(int theaterId);
    Task<List<Seat>> GetSeatsByShowtimeAsync(int showtimeId);
    Task<List<int>> GetOccupiedSeatIdsAsync(int showtimeId);
    Task AddSeatAsync(Seat seat);
    Task UpdateSeatAsync(Seat seat);
    Task DeleteSeatAsync(Seat seat);
    Task<IDbContextTransaction> BeginTransactionAsync();
}