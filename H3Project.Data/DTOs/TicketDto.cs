namespace H3Project.Data.DTOs;

public record TicketDto(int Id, int UserId, int ScheduleId, int SeatId, DateTime PurchaseDate, decimal Price);
