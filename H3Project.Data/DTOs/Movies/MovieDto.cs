namespace H3Project.Data.DTOs.Movies;

public record MovieDto(int Id, string Title, string Description, DateTime ReleaseDate, TimeSpan Duration, List<string> Genres);
