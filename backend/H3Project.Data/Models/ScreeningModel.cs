namespace H3Project.Data.Models;

public class ScreeningModel
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }

    // FK
    public int ScreenId { get; set; }
    public int MovieId { get; set; }

    // Nav Prop
    public ScreenModel? Screen { get; set; }
    public MovieModel? Movie { get; set; }
    public ICollection<TicketModel> Tickets { get; set; } = [];
    public ICollection<SeatAvailabilityModel> SeatAvailabilities { get; set; } = [];
}