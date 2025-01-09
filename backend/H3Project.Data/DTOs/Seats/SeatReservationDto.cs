namespace H3Project.Data.DTOs.Seats;

public class SeatReservationDto
{
    public int ShowtimeId { get; set; }
    public List<int> SeatIds { get; set; }
}