using H3Project.Data.Context;
using H3Project.Data.DTOs.Schedules;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IAppDbContext _context;

    public ScheduleController(IAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules()
    {
        var schedules = await _context.Schedules
            .AsNoTracking()
            .ToListAsync();

        var schedulesDtos = schedules.Select(MapToScheduleDto).ToList();

        return Ok(schedulesDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSchedule(int id)
    {
        var schedule = await _context.Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null)
        {
            return NotFound();
        }

        var scheduleDto = MapToScheduleDto(schedule);

        return Ok(scheduleDto);
    }

    private static ScheduleDto MapToScheduleDto(Schedule schedule)
    {
        return new ScheduleDto(schedule.Id, schedule.TheaterId, schedule.MovieId, schedule.ShowTime, schedule.EndTime);
    }
}