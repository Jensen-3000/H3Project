namespace H3Project.Data.Models;

public class Seat
{
    public int Id { get; set; }
    public string Row { get; set; }
    public int Number { get; set; }
    public bool IsAvailable { get; set; }

    public int TheaterId { get; set; }
    public Theater Theater { get; set; }
}
