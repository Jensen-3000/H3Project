namespace H3Project.Data.Models;

public class Cinema
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ICollection<Theater> Theaters { get; set; } = [];
}