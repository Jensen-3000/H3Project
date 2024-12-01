namespace H3Project.Data.DTOs.Movies;

public record MovieCreateDto(string Title, string Description, DateTime ReleaseDate, TimeSpan Duration, List<int> GenreIds);
