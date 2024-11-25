namespace H3Project.Data.Models.Domain;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public ICollection<Role> Roles { get; set; }
}