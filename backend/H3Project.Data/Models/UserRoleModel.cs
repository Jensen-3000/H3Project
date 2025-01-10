namespace H3Project.Data.Models;

public class UserRoleModel
{
    public int Id { get; set; }
    public string Role { get; set; }

    // Nav Prop
    public ICollection<UserModel> Users { get; set; } = [];
}
