namespace H3Project.Data.Models;

public class ScreenModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    // FK
    public int CinemaId { get; set; }

    // Nav Prop
    public CinemaModel Cinema { get; set; }
    public ICollection<SeatModel> Seats { get; set; } = [];
    public ICollection<ScreeningModel> Screenings { get; set; } = [];
}