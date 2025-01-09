using AutoMapper;
using H3Project.Data.DTOs.Users;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;
using H3Project.Data.Utilities;

namespace H3Project.Data.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDtoSimple>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserReadDtoSimple>>(users);
    }

    public async Task<UserReadDtoSimple?> GetUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserReadDtoSimple>(user);
    }

    public async Task<UserReadDto?> GetUserWithTicketsAsync(int id)
    {
        var user = await _userRepository.GetUserWithTicketsAsync(id);
        return user == null ? null : _mapper.Map<UserReadDto>(user);
    }

    public async Task<UserReadDto> CreateUserAsync(UserCreateDto userDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(userDto.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username is taken.");
        }

        var user = _mapper.Map<User>(userDto);
        user.PasswordHash = PasswordHasher.HashPassword(userDto.PasswordHash);

        await _userRepository.CreateAsync(user);
        var createdUser = await _userRepository.GetByIdAsync(user.Id);
        return _mapper.Map<UserReadDto>(createdUser);
    }

    public async Task UpdateUserAsync(int id, UserUpdateDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        _mapper.Map(userDto, user);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}