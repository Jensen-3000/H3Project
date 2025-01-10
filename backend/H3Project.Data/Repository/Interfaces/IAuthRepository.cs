using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IAuthRepository
{
    Task<UserModel?> GetByUsername(string username);
}
