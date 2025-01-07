namespace H3Project.Data.DTOs.Seats;

public record SeatUpdateDto(int Id, int TheaterId, string Row, int Number, bool IsAvailable);
