namespace H3Project.Data.DTOs.Schedules;

public record ScheduleDto(int Id, int TheaterId, int MovieId, DateTime ShowTime, DateTime EndTime);
