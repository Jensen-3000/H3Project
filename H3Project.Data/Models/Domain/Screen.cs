namespace H3Project.Data.Models.Domain;

public class Screen
{
    public int ScreenId { get; set; }
    public string Name { get; set; }
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }
    public ICollection<Seat> Seats { get; set; }
    public ICollection<Showtime> Showtimes { get; set; }
}