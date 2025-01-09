using H3Project.Data.DTOs.Schedules;

namespace H3Project.Data.Services.Interfaces;

public interface IScheduleService
{
    Task<List<ScheduleReadDto>> GetAllSchedulesAsync();
    Task<ScheduleReadDto?> GetScheduleByIdAsync(int id);
    Task<List<ScheduleReadDto>> GetShowtimesAsync(int movieId, int cinemaId);
    Task<ScheduleReadDto> CreateScheduleAsync(ScheduleCreateDto scheduleCreateDto);
    Task<bool> UpdateScheduleAsync(int id, ScheduleUpdateDto scheduleUpdateDto);
    Task<bool> DeleteScheduleAsync(int id);
}