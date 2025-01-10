using H3Project.Data.DTOs.Movies;

namespace H3Project.Data.DTOs.Genres;

public class GenreSimpleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class GenreDetailedDto : GenreSimpleDto
{
    public ICollection<MovieSimpleDto> Movies { get; set; }
}
