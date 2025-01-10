using H3Project.Data.DTOs.Screenings;
using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.DTOs.SeatAvailabilities;

public class SeatAvailabilitySimpleDto
{
    public bool IsAvailable { get; set; }
    public int ScreeningId { get; set; }
    public int SeatId { get; set; }
}

public class SeatAvailabilityDetailedDto : SeatAvailabilitySimpleDto
{
    public ScreeningDetailsDto Screening { get; set; }
    public SeatSimpleDto Seat { get; set; }
}
