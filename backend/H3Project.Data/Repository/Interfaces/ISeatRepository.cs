using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace H3Project.Data.Repository.Interfaces;

public interface ISeatRepository : IGenericRepository<SeatModel>
{
    Task<IEnumerable<SeatModel>> GetSeatsByScreenAsync(int screenId);
    Task<SeatModel?> GetSeatDetailsAsync(int id);
}
