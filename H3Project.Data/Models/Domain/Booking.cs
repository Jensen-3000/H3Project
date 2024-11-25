namespace H3Project.Data.Models.Domain;

public class Booking
{
    public int BookingId { get; set; }
    public DateTime BookingDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}