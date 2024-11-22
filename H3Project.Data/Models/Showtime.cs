namespace H3Project.Data.Models;

public class Showtime
{
    public int ShowtimeId { get; set; }
    public DateTime StartTime { get; set; }
    public int ScreenId { get; set; }
    public Screen Screen { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}