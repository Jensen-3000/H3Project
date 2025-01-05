using AutoMapper;
using H3Project.Data.Context;
using H3Project.Data.DTOs.Users;
using H3Project.Data.Models;
using H3Project.Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDtoSimple>>> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.UserRole)
            .ToListAsync();

        return Ok(_mapper.Map<List<UserReadDtoSimple>>(users));
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadDtoSimple>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserReadDtoSimple>(user));
    }

    // GET: api/Users/5/tickets
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<UserReadDto>> GetUserWithTickets(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Tickets)
                .ThenInclude(t => t.Schedule)
                .ThenInclude(s => s.Movie)
            .Include(u => u.Tickets)
                .ThenInclude(t => t.Schedule)
                .ThenInclude(s => s.Theater)
            .Include(u => u.Tickets)
                .ThenInclude(t => t.Seat)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserReadDto>(user));
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, [FromBody] UserUpdateDto userDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _mapper.Map(userDto, user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserReadDto>> PostUser(UserCreateDto userDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
        {
            return BadRequest("Username is taken.");
        }

        var user = _mapper.Map<User>(userDto);

        user.PasswordHash = PasswordHasher.HashPassword(userDto.PasswordHash);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var createdUser = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserReadDto>(createdUser));
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}
