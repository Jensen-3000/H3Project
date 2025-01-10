using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<UserModel?> GetUserWithDetails(int id);
    Task<UserModel?> GetByUsernameAsync(string username);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
}
