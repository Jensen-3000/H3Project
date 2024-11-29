namespace H3Project.Data.Models.Domain;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}