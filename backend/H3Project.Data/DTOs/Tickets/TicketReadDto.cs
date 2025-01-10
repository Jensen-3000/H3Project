using H3Project.Data.DTOs.Screenings;
using H3Project.Data.DTOs.Seats;

namespace H3Project.Data.DTOs.Tickets;

public class TicketSimpleDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}

public class TicketWithScreeningAndSeatDto : TicketSimpleDto
{
    public ScreeningDetailsDto Screening { get; set; }
    public SeatSimpleDto Seat { get; set; }
}
