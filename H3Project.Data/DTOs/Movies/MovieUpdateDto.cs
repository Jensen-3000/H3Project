namespace H3Project.Data.DTOs.Movies;

public record MovieUpdateDto(int Id, string Title, string Description, DateTime ReleaseDate, TimeSpan Duration, List<int> GenreIds);
