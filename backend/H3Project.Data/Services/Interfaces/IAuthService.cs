using H3Project.Data.DTOs.Users;

namespace H3Project.Data.Services.Interfaces;

public interface IAuthService
{
    Task<string?> GenerateTokenAsync(UserLoginDto loginDto);
}