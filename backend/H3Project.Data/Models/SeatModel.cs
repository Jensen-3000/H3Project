namespace H3Project.Data.Models;

public class SeatModel
{
    public int Id { get; set; }
    public string Row { get; set; }
    public int Number { get; set; }

    // FK
    public int ScreenId { get; set; }

    // Nav Prop
    public ScreenModel? Screen { get; set; }
    public ICollection<SeatAvailabilityModel> SeatAvailabilities { get; set; } = [];
}
