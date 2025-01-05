using AutoMapper;
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
    private readonly IMapper _mapper;

    public ScheduleController(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSchedules()
    {
        var scheduleModels = await _context.Schedules
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .AsNoTracking()
            .ToListAsync();

        var scheduleDtos = _mapper.Map<List<ScheduleReadDto>>(scheduleModels);

        return Ok(scheduleDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetScheduleById(int id)
    {
        var scheduleModel = await _context.Schedules
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (scheduleModel == null)
        {
            return NotFound();
        }

        var scheduleDto = _mapper.Map<ScheduleReadDto>(scheduleModel);

        return Ok(scheduleDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleCreateDto scheduleCreateDto)
    {
        var scheduleModel = _mapper.Map<Schedule>(scheduleCreateDto);

        _context.Schedules.Add(scheduleModel);
        await _context.SaveChangesAsync();

        var newScheduleModel = await _context.Schedules
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == scheduleModel.Id);

        var scheduleReadDto = _mapper.Map<ScheduleReadDto>(newScheduleModel);

        return CreatedAtAction(nameof(GetScheduleById), new { id = scheduleReadDto.Id }, scheduleReadDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSchedule(int id, ScheduleUpdateDto scheduleUpdateDto)
    {
        if (id != scheduleUpdateDto.Id)
        {
            return BadRequest();
        }

        var scheduleModel = await _context.Schedules.FindAsync(id);
        if (scheduleModel == null)
        {
            return NotFound();
        }

        _mapper.Map(scheduleUpdateDto, scheduleModel);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        var scheduleModel = await _context.Schedules.FindAsync(id);
        if (scheduleModel == null)
        {
            return NotFound();
        }

        _context.Schedules.Remove(scheduleModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
