namespace H3Project.Data.Models;

public class Seat
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; }
    public int ScreenId { get; set; }
    public Screen Screen { get; set; }
}