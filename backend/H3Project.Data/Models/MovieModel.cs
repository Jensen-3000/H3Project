namespace H3Project.Data.Models;

public class MovieModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public required string Slug { get; set; }
    public TimeSpan Duration { get; set; }

    // Nav Prop
    public ICollection<MovieGenre> MovieGenres { get; set; } = [];
    public ICollection<ScreeningModel> Screenings { get; set; } = [];
}