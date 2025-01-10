using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.DTOs.Screenings;
using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.DTOs.Screens;

public class ScreenSimpleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ScreenDetailedDto : ScreenSimpleDto
{
    public CinemaSimpleDto Cinema { get; set; }
    public ICollection<SeatSimpleDto> Seats { get; set; }
    public ICollection<ScreeningSimpleDto> Screenings { get; set; }
}
