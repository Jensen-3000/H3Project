namespace H3Project.Data.DTOs.Seats;

public record SeatReadDto(int Id, int TheaterId, string Row, int Number, bool IsAvailable);