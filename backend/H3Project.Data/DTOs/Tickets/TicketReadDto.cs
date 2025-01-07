using H3Project.Data.DTOs.Schedules;
using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.DTOs.Tickets;

public record TicketReadDto(int Id, DateTime PurchaseDate, decimal Price, ScheduleReadDto Schedule, SeatReadDto Seat);

public record TicketReadDtoSimple(DateTime PurchaseDate, decimal Price, ScheduleReadDto Schedule, SeatReadDto Seat);