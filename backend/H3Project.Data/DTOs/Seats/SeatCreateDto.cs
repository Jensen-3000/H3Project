namespace H3Project.Data.DTOs.Seats;

public record SeatCreateDto(int TheaterId, string Row, int Number, bool IsAvailable);