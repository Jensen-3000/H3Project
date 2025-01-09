namespace H3Project.Data.DTOs.Seats;

public class SeatGenerationRequestDto
{
    public int StartId { get; set; }
    public int Rows { get; set; }
    public int SeatsPerRow { get; set; }
}