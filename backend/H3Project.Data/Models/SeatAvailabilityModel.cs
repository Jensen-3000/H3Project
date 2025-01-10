namespace H3Project.Data.Models;

public class SeatAvailabilityModel
{
    public bool IsAvailable { get; set; }

    // FK
    public int ScreeningId { get; set; }
    public int SeatId { get; set; }


    // Nav Prop
    public ScreeningModel? Screening { get; set; }

    public SeatModel? Seat { get; set; }
}
