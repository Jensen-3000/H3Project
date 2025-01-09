using H3Project.Data.DTOs.Users;

namespace H3Project.Data.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDtoSimple>> GetAllUsersAsync();
    Task<UserReadDtoSimple?> GetUserAsync(int id);
    Task<UserReadDto?> GetUserWithTicketsAsync(int id);
    Task<UserReadDto> CreateUserAsync(UserCreateDto userDto);
    Task UpdateUserAsync(int id, UserUpdateDto userDto);
    Task DeleteUserAsync(int id);
}