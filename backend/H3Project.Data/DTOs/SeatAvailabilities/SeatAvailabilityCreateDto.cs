namespace H3Project.Data.DTOs.SeatAvailabilities;

public class SeatAvailabilityCreateDto
{
    public bool IsAvailable { get; set; }
    public int ScreeningId { get; set; }
    public int SeatId { get; set; }
}
