namespace H3Project.Data.Models;

public class TicketModel
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    // FK
    public int UserId { get; set; }
    public int ScreeningId { get; set; }
    public int SeatId { get; set; }

    // Nav Prop
    public UserModel? User { get; set; }
    public ScreeningModel? Screening { get; set; }
    public SeatModel? Seat { get; set; }
}