namespace H3Project.Data.Models;

public class CinemaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    // Nav Prop
    public ICollection<ScreenModel> Screens { get; set; } = [];
}
