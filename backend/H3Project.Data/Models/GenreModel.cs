namespace H3Project.Data.Models;

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Nav Prop
    public ICollection<MovieGenre> MovieGenres { get; set; } = [];
}