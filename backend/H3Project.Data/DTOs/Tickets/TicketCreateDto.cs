namespace H3Project.Data.DTOs.Tickets;

public class TicketCreateDto
{
    public decimal Price { get; set; }
    public int UserId { get; set; }
    public int ScreeningId { get; set; }
    public int SeatId { get; set; }
}
