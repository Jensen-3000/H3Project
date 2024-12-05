namespace H3Project.Data.DTOs.Tickets;

public record TicketDto(int Id, int UserId, int ScheduleId, int SeatId, DateTime PurchaseDate, decimal Price);
