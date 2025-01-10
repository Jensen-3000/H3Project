using H3Project.Data.DTOs.UserRoles;

namespace H3Project.Data.Services.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRoleSimpleDto>> GetAllAsync();
    Task<UserRoleWithUsersDto> GetByIdAsync(int id);
    Task<UserRoleSimpleDto> CreateAsync(UserRoleCreateDto createDto);
    Task UpdateAsync(int id, UserRoleUpdateDto updateDto);
    Task DeleteAsync(int id);
}
