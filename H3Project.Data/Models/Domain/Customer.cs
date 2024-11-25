namespace H3Project.Data.Models.Domain;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}