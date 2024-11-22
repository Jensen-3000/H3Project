namespace H3Project.Data.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public ICollection<Showtime> Showtimes { get; set; }
    public ICollection<Genre> Genres { get; set; }
}