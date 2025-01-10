using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IUserRoleRepository : IGenericRepository<UserRoleModel>
{
    Task<UserRoleModel?> GetRoleWithUsersAsync(int id);
    Task<UserRoleModel?> GetRoleByNameAsync(string name);
}
