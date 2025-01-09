using AutoMapper;
using H3Project.Data.DTOs.Schedules;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleReadDto>> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllSchedulesAsync();
        return _mapper.Map<List<ScheduleReadDto>>(schedules);
    }

    public async Task<ScheduleReadDto?> GetScheduleByIdAsync(int id)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
        return schedule == null ? null : _mapper.Map<ScheduleReadDto>(schedule);
    }

    public async Task<List<ScheduleReadDto>> GetShowtimesAsync(int movieId, int cinemaId)
    {
        var schedules = await _scheduleRepository.GetShowtimesAsync(movieId, cinemaId);
        return _mapper.Map<List<ScheduleReadDto>>(schedules);
    }

    public async Task<ScheduleReadDto> CreateScheduleAsync(ScheduleCreateDto scheduleCreateDto)
    {
        var schedule = _mapper.Map<Schedule>(scheduleCreateDto);
        await _scheduleRepository.AddScheduleAsync(schedule);

        var newSchedule = await _scheduleRepository.GetScheduleByIdAsync(schedule.Id);
        return _mapper.Map<ScheduleReadDto>(newSchedule);
    }

    public async Task<bool> UpdateScheduleAsync(int id, ScheduleUpdateDto scheduleUpdateDto)
    {
        if (id != scheduleUpdateDto.Id)
        {
            return false;
        }

        var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
        if (schedule == null)
        {
            return false;
        }

        _mapper.Map(scheduleUpdateDto, schedule);
        await _scheduleRepository.UpdateScheduleAsync(schedule);

        return true;
    }

    public async Task<bool> DeleteScheduleAsync(int id)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
        if (schedule == null)
        {
            return false;
        }

        await _scheduleRepository.DeleteScheduleAsync(schedule);
        return true;
    }
}