using H3Project.Data.DTOs.Users;

namespace H3Project.Data.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserSimpleDto>> GetAllAsync();
    Task<UserWithRoleAndTicketsDto> GetByIdAsync(int id);
    Task<UserSimpleDto> CreateAsync(UserCreateDto createDto);
    Task UpdateAsync(int id, UserUpdateDto updateDto);
    Task DeleteAsync(int id);
}
