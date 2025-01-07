using H3Project.Data.DTOs.Tickets;
using H3Project.Data.DTOs.UserRoles;

namespace H3Project.Data.DTOs.Users;

public class UserReadDtoSimple()
{
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRoleDto Role { get; set; }

}

public class UserReadDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRoleDto Role { get; set; }
    public IEnumerable<TicketReadDto> Tickets { get; set; } = [];
}
