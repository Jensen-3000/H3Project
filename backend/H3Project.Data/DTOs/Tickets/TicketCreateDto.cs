namespace H3Project.Data.DTOs.Tickets;

public record TicketCreateDto(DateTime PurchaseDate, decimal Price, int ScheduleId, int SeatId);