namespace H3Project.Data.DTOs.Schedules;

//public record ScheduleReadDto(int Id, int TheaterId, int MovieId, DateTime ShowTime, DateTime EndTime);

//public record ScheduleReadDto(int Id, int TheaterId, string TheaterName, int MovieId, string MovieTitle, DateTime ShowTime, DateTime EndTime);

//public class ScheduleDto
//{
//    public int Id { get; set; }
//    public DateTime ShowTime { get; set; }
//    public DateTime EndTime { get; set; }
//    public decimal BasePrice { get; set; }
//    public int TheaterId { get; set; }
//}

public class ScheduleReadDto
{
    public int Id { get; set; }
    public int TheaterId { get; set; }
    public string TheaterName { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime ShowTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal BasePrice { get; set; }
}
