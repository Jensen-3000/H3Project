using H3Project.Data.DTOs.Screens;

namespace H3Project.Data.DTOs.Cinemas;

public class CinemaSimpleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class CinemaDetailedDto : CinemaSimpleDto
{
    public ICollection<ScreenSimpleDto> Screens { get; set; }
}
