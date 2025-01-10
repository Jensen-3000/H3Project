using AutoMapper;
using H3Project.Data.DTOs.Users;
using H3Project.Data.Models;
using H3Project.Data.Services.Interfaces;
using H3Project.Data.Utilities;

namespace H3Project.Data.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserSimpleDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserSimpleDto>>(users);
    }

    public async Task<UserWithRoleAndTicketsDto> GetByIdAsync(int id)
    {
        var user = await _repository.GetUserWithDetails(id);
        return _mapper.Map<UserWithRoleAndTicketsDto>(user);
    }

    public async Task<UserSimpleDto> CreateAsync(UserCreateDto createDto)
    {
        if (await _repository.UsernameExistsAsync(createDto.Username))
        {
            throw new InvalidOperationException("Username already exists");
        }

        if (await _repository.EmailExistsAsync(createDto.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = _mapper.Map<UserModel>(createDto);
        user.Password = PasswordHasher.HashPassword(createDto.Password);

        await _repository.AddAsync(user);
        return _mapper.Map<UserSimpleDto>(user);
    }

    public async Task UpdateAsync(int id, UserUpdateDto updateDto)
    {
        var user = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, user);
        await _repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(user);
    }
}
