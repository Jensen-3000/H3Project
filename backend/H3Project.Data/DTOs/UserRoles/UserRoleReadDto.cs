using H3Project.Data.DTOs.Users;

namespace H3Project.Data.DTOs.UserRoles;

public class UserRoleSimpleDto
{
    public int Id { get; set; }
    public string Role { get; set; }
}

public class UserRoleWithUsersDto : UserRoleSimpleDto
{
    public ICollection<UserSimpleDto> Users { get; set; }
}
