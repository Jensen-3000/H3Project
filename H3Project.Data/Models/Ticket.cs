namespace H3Project.Data.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public int ShowtimeId { get; set; }
    public Showtime Showtime { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; }
}