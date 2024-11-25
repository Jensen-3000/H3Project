namespace H3Project.Data.Models.Domain;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}