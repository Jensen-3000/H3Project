using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IScheduleRepository
{
    Task<List<Schedule>> GetSchedulesAsync();
    Task<Schedule> GetScheduleByIdAsync(int id);
    Task AddScheduleAsync(Schedule schedule);
    Task UpdateScheduleAsync(Schedule schedule);
    Task DeleteScheduleAsync(Schedule schedule);
    Task<Schedule> FindScheduleByIdAsync(int id);
    bool ScheduleExists(int id);
}