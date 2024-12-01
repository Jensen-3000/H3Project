namespace H3Project.Data.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = [];
}
