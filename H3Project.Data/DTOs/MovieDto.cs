namespace H3Project.Data.DTOs;

public record MovieDto(int Id, int GenreId, string Title, string Description, DateTime ReleaseDate, TimeSpan Duration);
