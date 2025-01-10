namespace H3Project.Data.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // FK
    public int UserRoleId { get; set; }

    // Nav Prop
    public UserRoleModel? UserRole { get; set; }
    public ICollection<TicketModel> Tickets { get; set; } = [];
}
