using H3Project.Data.DTOs.Tickets;
using H3Project.Data.DTOs.UserRoles;

namespace H3Project.Data.DTOs.Users;

public class UserSimpleDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class UserWithRoleAndTicketsDto : UserSimpleDto
{
    public UserRoleSimpleDto UserRole { get; set; }
    public ICollection<TicketWithScreeningAndSeatDto> Tickets { get; set; }
}
