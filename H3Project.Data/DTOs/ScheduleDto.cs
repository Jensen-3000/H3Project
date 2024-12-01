namespace H3Project.Data.DTOs;

public record ScheduleDto(int Id, int TheaterId, int MovieId, DateTime ShowTime, DateTime EndTime);
