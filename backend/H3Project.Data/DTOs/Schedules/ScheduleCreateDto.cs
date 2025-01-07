namespace H3Project.Data.DTOs.Schedules;

public record ScheduleCreateDto(int TheaterId, int MovieId, DateTime ShowTime, DateTime EndTime);
