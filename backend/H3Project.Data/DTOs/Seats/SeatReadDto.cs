using H3Project.Data.DTOs.Screens;

namespace H3Project.Data.DTOs.Seats;

public class SeatSimpleDto
{
    public int Id { get; set; }
    public string Row { get; set; }
    public int Number { get; set; }
}

public class SeatDetailedDto : SeatSimpleDto
{
    public ScreenSimpleDto Screen { get; set; }
}
