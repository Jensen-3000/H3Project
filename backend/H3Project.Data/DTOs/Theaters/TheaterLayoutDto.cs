using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.DTOs.Theaters;

public class TheaterLayoutDto
{
    public int SeatsPerRow { get; set; }
    public IEnumerable<SeatDto> Seats { get; set; }
}