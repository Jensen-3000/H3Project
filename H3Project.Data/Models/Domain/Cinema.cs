namespace H3Project.Data.Models.Domain;

public class Cinema
{
    public int CinemaId { get; set; }
    public string Name { get; set; }
    public ICollection<Screen> Screens { get; set; }
    public CinemaDetail CinemaDetail { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
