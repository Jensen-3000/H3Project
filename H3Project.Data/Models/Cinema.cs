namespace H3Project.Data.Models;

public class Cinema
{
    public int CinemaId { get; set; }
    public string Name { get; set; }
    public ICollection<Screen> Screens { get; set; }
    public CinemaDetail CinemaDetail { get; set; }
}