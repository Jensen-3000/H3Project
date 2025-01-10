namespace H3Project.Data.DTOs.Movies;

public class MovieUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Slug { get; set; }
    public TimeSpan Duration { get; set; }
    public List<int> GenreIds { get; set; } = [];
}
