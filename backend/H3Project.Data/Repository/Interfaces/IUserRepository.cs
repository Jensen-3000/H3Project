using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<User> FindUserByIdAsync(int id);
    bool UserExists(int id);
}