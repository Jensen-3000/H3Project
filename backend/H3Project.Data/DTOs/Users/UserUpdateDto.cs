namespace H3Project.Data.DTOs.Users;

public record UserUpdateDto(string Username, string Email, string PasswordHash, int UserRoleId);