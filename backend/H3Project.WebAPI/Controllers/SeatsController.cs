using AutoMapper;
using H3Project.Data.Context;
using H3Project.Data.DTOs.Seats;
using H3Project.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using H3Project.Data.DTOs.Theaters;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<SeatsController> _logger;

    public SeatsController(AppDbContext context, IMapper mapper, ILogger<SeatsController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Seats
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeatReadDto>>> GetSeats()
    {
        var seats = await _context.Seats.ToListAsync();
        return _mapper.Map<List<SeatReadDto>>(seats);
    }

    // GET: api/Seats/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SeatReadDto>> GetSeat(int id)
    {
        var seat = await _context.Seats.FindAsync(id);

        if (seat == null)
        {
            return NotFound();
        }

        return _mapper.Map<SeatReadDto>(seat);
    }

    [HttpGet("theater/{theaterId}")]
    public IActionResult GetSeatsByTheater(int theaterId)
    {
        // Get seats for specific theater
        var seats = _context.Seats
            .Where(s => s.TheaterId == theaterId)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Number)
            .ToList();

        var seatLayout = seats
            .GroupBy(s => s.Row)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Row = g.Key,
                Seats = g.OrderBy(s => s.Number).ToList()
            })
            .ToList();

        return Ok(seatLayout);
    }

    [HttpGet("showtime/{showtimeId}/layout")]
    public async Task<ActionResult<TheaterLayoutDto>> GetTheaterLayout(int showtimeId)
    {
        // Load schedule and theater with seats
        var schedule = await _context.Schedules
            .Include(s => s.Theater)
            .FirstOrDefaultAsync(s => s.Id == showtimeId);

        if (schedule == null)
            return NotFound();

        // Load all seats for theater
        var seats = await _context.Seats
            .Where(s => s.TheaterId == schedule.TheaterId)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Number)
            .ToListAsync();

        // Load occupied seats for this schedule
        var occupiedSeatIds = await _context.Tickets
            .Where(t => t.ScheduleId == showtimeId)
            .Select(t => t.SeatId)
            .ToHashSetAsync();

        var seatsPerRow = seats
            .GroupBy(s => s.Row)
            .Max(g => g.Count());

        var seatDtos = seats.Select(s => new SeatDto
        {
            Id = s.Id,
            Row = s.Row,
            Number = s.Number,
            Status = !s.IsAvailable ? "disabled" :
                occupiedSeatIds.Contains(s.Id) ? "occupied" :
                "available",
            Price = schedule.BasePrice
        });

        return Ok(new TheaterLayoutDto
        {
            SeatsPerRow = seatsPerRow,
            Seats = seatDtos
        });
    }

    // PUT: api/Seats/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeat(int id, SeatUpdateDto seatDto)
    {
        if (id != seatDto.Id)
        {
            return BadRequest();
        }

        var seat = _mapper.Map<Seat>(seatDto);
        _context.Entry(seat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SeatExists(id))
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

    [Authorize]
    [HttpPost("reserve")]
    public async Task<ActionResult<bool>> ReserveSeats([FromBody] SeatReservationDto reservation)
    {
        try
        {
            // Get username from claims
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("No username claim found in token");
                return Unauthorized();
            }

            // Verify user exists by username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                _logger.LogWarning("User not found for username: {Username}", username);
                return Unauthorized();
            }

            // Get schedule
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == reservation.ShowtimeId);

            if (schedule == null)
            {
                _logger.LogWarning("Schedule not found: {ShowtimeId}", reservation.ShowtimeId);
                return NotFound("Schedule not found");
            }

            // Check seats
            var requestedSeats = await _context.Seats
                .Where(s => reservation.SeatIds.Contains(s.Id))
                .ToListAsync();

            if (requestedSeats.Count != reservation.SeatIds.Count)
            {
                _logger.LogWarning("Not all requested seats were found");
                return BadRequest("One or more seats not found");
            }

            if (requestedSeats.Any(s => !s.IsAvailable))
            {
                _logger.LogWarning("Some seats are not available");
                return BadRequest("One or more seats are not available");
            }

            // Check existing tickets
            var existingTickets = await _context.Tickets
                .AnyAsync(t => t.ScheduleId == reservation.ShowtimeId &&
                              reservation.SeatIds.Contains(t.SeatId));

            if (existingTickets)
            {
                _logger.LogWarning("Some seats are already reserved");
                return BadRequest("One or more seats are already reserved");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tickets = requestedSeats.Select(seat => new Ticket
                {
                    ScheduleId = reservation.ShowtimeId,
                    SeatId = seat.Id,
                    UserId = user.Id,
                    PurchaseDate = DateTime.UtcNow,
                    Price = schedule.BasePrice
                });

                _context.Tickets.AddRange(tickets);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Successfully reserved {Count} seats for user {Username}",
                    reservation.SeatIds.Count, username);
                return Ok(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Failed to reserve seats for user {Username}", username);
                return StatusCode(500, "Failed to reserve seats");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in ReserveSeats");
            return StatusCode(500, "An unexpected error occurred");
        }
    }

    // POST: api/Seats
    [HttpPost]
    public async Task<ActionResult<SeatReadDto>> PostSeat(SeatCreateDto seatDto)
    {
        var seat = _mapper.Map<Seat>(seatDto);
        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSeat), new { id = seat.Id }, _mapper.Map<SeatReadDto>(seat));
    }

    // DELETE: api/Seats/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeat(int id)
    {
        var seat = await _context.Seats.FindAsync(id);
        if (seat == null)
        {
            return NotFound();
        }

        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SeatExists(int id)
    {
        return _context.Seats.Any(e => e.Id == id);
    }
}
