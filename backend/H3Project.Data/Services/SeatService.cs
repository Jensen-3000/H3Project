using AutoMapper;
using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;
using H3Project.Data.Utilities;
using Microsoft.Extensions.Logging;

namespace H3Project.Data.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SeatService> _logger;

    public SeatService(
        ISeatRepository seatRepository,
        IUserRepository userRepository,
        ITicketRepository ticketRepository,
        IScheduleRepository scheduleRepository,
        IMapper mapper,
        ILogger<SeatService> logger)
    {
        _seatRepository = seatRepository;
        _userRepository = userRepository;
        _ticketRepository = ticketRepository;
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<SeatReadDto>> GetAllSeatsAsync()
    {
        var seats = await _seatRepository.GetAllSeatsAsync();
        return _mapper.Map<List<SeatReadDto>>(seats);
    }

    public async Task<SeatReadDto?> GetSeatByIdAsync(int id)
    {
        var seat = await _seatRepository.GetSeatByIdAsync(id);
        return seat == null ? null : _mapper.Map<SeatReadDto>(seat);
    }

    public async Task<List<SeatReadDto>> GetSeatsByTheaterAsync(int theaterId)
    {
        var seats = await _seatRepository.GetSeatsByTheaterAsync(theaterId);
        return _mapper.Map<List<SeatReadDto>>(seats);
    }

    public async Task<TheaterLayoutDto?> GetTheaterLayoutAsync(int showtimeId)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(showtimeId);
        if (schedule == null)
        {
            return null;
        }

        var seats = await _seatRepository.GetSeatsByTheaterAsync(schedule.TheaterId);
        var occupiedSeatIds = await _seatRepository.GetOccupiedSeatIdsAsync(showtimeId);

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

        return new TheaterLayoutDto
        {
            SeatsPerRow = seatsPerRow,
            Seats = seatDtos
        };
    }

    public async Task<bool> ReserveSeatsAsync(SeatReservationDto reservation, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            _logger.LogWarning("User not found for username: {Username}", username);
            return false;
        }

        var schedule = await _scheduleRepository.GetScheduleByIdAsync(reservation.ShowtimeId);
        if (schedule == null)
        {
            _logger.LogWarning("Schedule not found: {ShowtimeId}", reservation.ShowtimeId);
            return false;
        }

        var requestedSeats = await _seatRepository.GetSeatsByShowtimeAsync(reservation.ShowtimeId);
        if (requestedSeats.Count != reservation.SeatIds.Count)
        {
            _logger.LogWarning("Not all requested seats were found");
            return false;
        }

        if (requestedSeats.Any(s => !s.IsAvailable))
        {
            _logger.LogWarning("Some seats are not available");
            return false;
        }

        var existingTickets = await _ticketRepository.GetAllTicketsAsync();
        if (existingTickets.Any(t => t.ScheduleId == reservation.ShowtimeId && reservation.SeatIds.Contains(t.SeatId)))
        {
            _logger.LogWarning("Some seats are already reserved");
            return false;
        }

        await using var transaction = await _seatRepository.BeginTransactionAsync();
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

            await _ticketRepository.AddTicketsAsync(tickets);
            await transaction.CommitAsync();

            _logger.LogInformation("Successfully reserved {Count} seats for user {Username}",
                reservation.SeatIds.Count, username);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to reserve seats for user {Username}", username);
            return false;
        }
    }

    public async Task<SeatReadDto> CreateSeatAsync(SeatCreateDto seatCreateDto)
    {
        var seat = _mapper.Map<Seat>(seatCreateDto);
        await _seatRepository.AddSeatAsync(seat);
        return _mapper.Map<SeatReadDto>(seat);
    }

    public async Task<bool> UpdateSeatAsync(int id, SeatUpdateDto seatUpdateDto)
    {
        if (id != seatUpdateDto.Id)
        {
            return false;
        }

        var seat = await _seatRepository.GetSeatByIdAsync(id);
        if (seat == null)
        {
            return false;
        }

        _mapper.Map(seatUpdateDto, seat);
        await _seatRepository.UpdateSeatAsync(seat);

        return true;
    }

    public async Task<bool> DeleteSeatAsync(int id)
    {
        var seat = await _seatRepository.GetSeatByIdAsync(id);
        if (seat == null)
        {
            return false;
        }

        await _seatRepository.DeleteSeatAsync(seat);
        return true;
    }

    public Task<List<Seat>> GenerateSeatsAsync(int theaterId, SeatGenerationRequestDto request)
    {
        var seats = SeatGenerator.GenerateTheaterSeats(theaterId, request.StartId, request.Rows, request.SeatsPerRow);
        return Task.FromResult(seats);
    }
}