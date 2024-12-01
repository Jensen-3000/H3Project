using H3Project.Data.DTOs;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemaController : ControllerBase
{
    private readonly IRepository<Cinema> _cinemaRepository;

    public CinemaController(IRepository<Cinema> cinemaRepository)
    {
        _cinemaRepository = cinemaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCinemas()
    {
        var cinemas = await _cinemaRepository.GetAllAsync();
        var cinemaDtos = cinemas.Select(c => new CinemaDto(c.Id, c.Name, c.Address));

        return Ok(cinemaDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCinema(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        var cinemaDto = new CinemaDto(cinema.Id, cinema.Name, cinema.Address);

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

        await _cinemaRepository.AddAsync(cinema);
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

        var cinema = await _cinemaRepository.GetByIdAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        cinema.Name = cinemaDto.Name;
        cinema.Address = cinemaDto.Address;

        await _cinemaRepository.UpdateAsync(cinema);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }

        await _cinemaRepository.DeleteAsync(id);

        return NoContent();
    }
}
