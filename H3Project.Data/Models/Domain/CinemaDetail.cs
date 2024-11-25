namespace H3Project.Data.Models.Domain;

public class CinemaDetail
{
    public int CinemaDetailId { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }
}