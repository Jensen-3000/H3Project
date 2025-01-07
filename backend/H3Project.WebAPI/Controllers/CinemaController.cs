using H3Project.Data.Context;
using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemaController : ControllerBase
{
    private readonly IAppDbContext _context;

    public CinemaController(IAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCinemas()
    {
        var cinemaModels = await _context.Cinemas
            .AsNoTracking()
            .ToListAsync();

        var cinemaDtos = cinemaModels.Select(MapModelToReadDto).ToList();

        return Ok(cinemaDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCinemaById(int id)
    {
        var cinemaModel = await _context.Cinemas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cinemaModel == null)
        {
            return NotFound();
        }

        var cinemaDto = MapModelToReadDto(cinemaModel);

        return Ok(cinemaDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCinema(CinemaCreateDto cinemaCreateDto)
    {
        var cinemaModel = MapCreateDtoToModel(cinemaCreateDto);

        _context.Cinemas.Add(cinemaModel);
        await _context.SaveChangesAsync();

        var newCinemaDto = MapModelToReadDto(cinemaModel);

        return CreatedAtAction(nameof(GetCinemaById), new { id = newCinemaDto.Id }, newCinemaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCinema(int id, CinemaUpdateDto cinemaUpdateDto)
    {
        if (id != cinemaUpdateDto.Id)
        {
            return BadRequest();
        }

        var cinemaModel = await _context.Cinemas.FindAsync(id);
        if (cinemaModel == null)
        {
            return NotFound();
        }

        cinemaModel.Name = cinemaUpdateDto.Name;
        cinemaModel.Address = cinemaUpdateDto.Address;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var cinemaModel = await _context.Cinemas.FindAsync(id);
        if (cinemaModel == null)
        {
            return NotFound();
        }

        _context.Cinemas.Remove(cinemaModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static CinemaReadDto MapModelToReadDto(Cinema cinema) => new(cinema.Id, cinema.Name, cinema.Address);

    private static Cinema MapCreateDtoToModel(CinemaCreateDto cinemaCreateDto) => new()
    {
        Name = cinemaCreateDto.Name,
        Address = cinemaCreateDto.Address
    };
}
