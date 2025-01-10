using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CinemasController : ControllerBase
{
    private readonly ICinemaService _cinemaService;

    public CinemasController(ICinemaService cinemaService)
    {
        _cinemaService = cinemaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CinemaSimpleDto>>> GetAll()
    {
        return Ok(await _cinemaService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CinemaDetailedDto>> GetById(int id)
    {
        return Ok(await _cinemaService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<CinemaSimpleDto>> Create(CinemaCreateDto createDto)
    {
        var cinema = await _cinemaService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = cinema.Id }, cinema);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CinemaUpdateDto updateDto)
    {
        await _cinemaService.UpdateAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cinemaService.DeleteAsync(id);
        return NoContent();
    }
}
