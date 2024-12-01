namespace H3Project.Data.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }

    public ICollection<Genre> Genres { get; set; } = [];
    public ICollection<Schedule> Schedules { get; set; } = [];
}
