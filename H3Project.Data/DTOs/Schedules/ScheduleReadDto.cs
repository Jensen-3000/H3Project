namespace H3Project.Data.DTOs.Schedules;

//public record ScheduleReadDto(int Id, int TheaterId, int MovieId, DateTime ShowTime, DateTime EndTime);

public record ScheduleReadDto(int Id, int TheaterId, string TheaterName, int MovieId, string MovieTitle, DateTime ShowTime, DateTime EndTime);
