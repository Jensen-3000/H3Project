using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IScheduleRepository
{
    Task<List<Schedule>> GetAllSchedulesAsync();
    Task<Schedule?> GetScheduleByIdAsync(int id);
    Task<List<Schedule>> GetShowtimesAsync(int movieId, int cinemaId);
    Task AddScheduleAsync(Schedule schedule);
    Task UpdateScheduleAsync(Schedule schedule);
    Task DeleteScheduleAsync(Schedule schedule);
}