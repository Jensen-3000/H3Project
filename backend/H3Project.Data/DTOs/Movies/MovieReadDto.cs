using H3Project.Data.DTOs.Schedules;

namespace H3Project.Data.DTOs.Movies;

//public record MovieReadDto(int Id, string Title, string Description, DateOnly ReleaseDate, TimeSpan Duration, List<string> Genres);

public class MovieReadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public ICollection<string> Genres { get; set; }
    public ICollection<ScheduleReadDto> CurrentSchedules { get; set; }
}