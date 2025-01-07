namespace H3Project.Data.Models;

public class Theater
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }

    public ICollection<Seat> Seats { get; set; } = [];
    public ICollection<Schedule> Schedules { get; set; } = [];
}
