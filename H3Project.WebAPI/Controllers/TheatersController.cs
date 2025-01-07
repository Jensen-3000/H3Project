using AutoMapper;
using H3Project.Data.Context;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheatersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TheatersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Theaters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TheaterReadDto>>> GetTheaters()
    {
        var theaters = await _context.Theaters.ToListAsync();
        return _mapper.Map<List<TheaterReadDto>>(theaters);
    }

    // GET: api/Theaters/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TheaterReadDto>> GetTheater(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);

        if (theater == null)
        {
            return NotFound();
        }

        return _mapper.Map<TheaterReadDto>(theater);
    }

    // PUT: api/Theaters/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTheater(int id, TheaterUpdateDto theaterDto)
    {
        if (id != theaterDto.Id)
        {
            return BadRequest();
        }

        var theater = _mapper.Map<Theater>(theaterDto);
        _context.Entry(theater).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TheaterExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Theaters
    [HttpPost]
    public async Task<ActionResult<TheaterReadDto>> PostTheater(TheaterCreateDto theaterDto)
    {
        var theater = _mapper.Map<Theater>(theaterDto);
        _context.Theaters.Add(theater);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTheater), new { id = theater.Id }, _mapper.Map<TheaterReadDto>(theater));
    }

    // DELETE: api/Theaters/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheater(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null)
        {
            return NotFound();
        }

        _context.Theaters.Remove(theater);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TheaterExists(int id)
    {
        return _context.Theaters.Any(e => e.Id == id);
    }
}
