namespace H3Project.Data.DTOs;

public record SeatDto(int Id, int TheaterId, string Row, int Number, bool IsAvailable);