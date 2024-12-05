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
    public async Task<IActionResult> GetCinemas()
    {
        var cinemaDtos = await _context.Cinemas
            .AsNoTracking()
            .Select(c => new CinemaDto(c.Id, c.Name, c.Address))
            .ToListAsync();

        return Ok(cinemaDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCinema(int id)
    {
        var cinemaDto = await _context.Cinemas
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CinemaDto(c.Id, c.Name, c.Address))
            .FirstOrDefaultAsync();

        if (cinemaDto == null)
        {
            return NotFound();
        }

        return Ok(cinemaDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostCinema(CinemaDto cinemaDto)
    {
        var cinema = new Cinema
        {
            Name = cinemaDto.Name,
            Address = cinemaDto.Address
        };

        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();

        var newCinemaDto = new CinemaDto(cinema.Id, cinema.Name, cinema.Address);

        return CreatedAtAction(nameof(GetCinema), new { id = newCinemaDto.Id }, newCinemaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCinema(int id, CinemaDto cinemaDto)
    {
        if (id != cinemaDto.Id)
        {
            return BadRequest();
        }

        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        cinema.Name = cinemaDto.Name;
        cinema.Address = cinemaDto.Address;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
