namespace H3Project.Data.DTOs.Tickets;

public record TicketUpdateDto(int Id, DateTime PurchaseDate, decimal Price, int ScheduleId, int SeatId);
