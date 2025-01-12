using H3Project.Data.DTOs.SeatAvailabilities;

namespace H3Project.Data.DTOs.Screenings;

public class ScreeningSimpleDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public string CinemaName { get; set; }
    public string CinemaAddress { get; set; }
}

public class ScreeningDetailsDto
{
    public string MovieTitle { get; set; }
    public string ScreenName { get; set; }
}

public class ScreeningAvailableSeatsDto : ScreeningDetailsDto
{
    public ICollection<SeatAvailabilitySimpleDto> SeatAvailabilities { get; set; }
}
