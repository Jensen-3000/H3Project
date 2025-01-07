namespace H3Project.Data.Models;

public class Ticket
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }

    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; }

    public int SeatId { get; set; }
    public Seat Seat { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
