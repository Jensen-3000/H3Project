using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
}