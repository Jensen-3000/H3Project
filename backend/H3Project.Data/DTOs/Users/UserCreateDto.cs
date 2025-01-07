namespace H3Project.Data.DTOs.Users;

public record UserCreateDto(string Username, string Email, string PasswordHash, int UserRoleId);