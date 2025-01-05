namespace H3Project.Data.DTOs.Movies;

public record MovieReadDto(int Id, string Title, string Description, DateOnly ReleaseDate, TimeSpan Duration, List<string> Genres);