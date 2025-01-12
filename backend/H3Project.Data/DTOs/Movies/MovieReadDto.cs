using H3Project.Data.DTOs.Genres;
using H3Project.Data.DTOs.Screenings;

namespace H3Project.Data.DTOs.Movies;

public class MovieSimpleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public TimeSpan Duration { get; set; }
}

public class MovieDetailedDto : MovieSimpleDto
{
    public string Description { get; set; }
    public string Slug { get; set; }
    public ICollection<GenreSimpleDto> Genres { get; set; } = [];
    public ICollection<ScreeningSimpleDto> Screenings { get; set; } = [];
}
