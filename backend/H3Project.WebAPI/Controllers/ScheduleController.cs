using H3Project.Data.DTOs.Schedules;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSchedules()
    {
        var schedules = await _scheduleService.GetAllSchedulesAsync();
        return Ok(schedules);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetScheduleById(int id)
    {
        var schedule = await _scheduleService.GetScheduleByIdAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    [HttpGet("movie/{movieId}/cinema/{cinemaId}")]
    public async Task<IActionResult> GetShowtimes(int movieId, int cinemaId)
    {
        var showtimes = await _scheduleService.GetShowtimesAsync(movieId, cinemaId);
        return Ok(showtimes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleCreateDto scheduleCreateDto)
    {
        var newSchedule = await _scheduleService.CreateScheduleAsync(scheduleCreateDto);
        return CreatedAtAction(nameof(GetScheduleById), new { id = newSchedule.Id }, newSchedule);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSchedule(int id, ScheduleUpdateDto scheduleUpdateDto)
    {
        var result = await _scheduleService.UpdateScheduleAsync(id, scheduleUpdateDto);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        var result = await _scheduleService.DeleteScheduleAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}