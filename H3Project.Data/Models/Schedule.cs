namespace H3Project.Data.Models;

public class Schedule
{
    public int Id { get; set; }
    public DateTime ShowTime { get; set; }
    public DateTime EndTime { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int TheaterId { get; set; }
    public Theater Theater { get; set; }
}
