namespace H3Project.Data.Models;

public class MovieGenre
{
    // Composite Key
    public int MovieId { get; set; }
    public MovieModel Movie { get; set; }

    public int GenreId { get; set; }
    public GenreModel Genre { get; set; }
}