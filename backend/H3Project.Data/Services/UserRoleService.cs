using AutoMapper;
using H3Project.Data.DTOs.UserRoles;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;
    private readonly IMapper _mapper;

    public UserRoleService(IUserRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserRoleSimpleDto>> GetAllAsync()
    {
        var roles = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserRoleSimpleDto>>(roles);
    }

    public async Task<UserRoleWithUsersDto> GetByIdAsync(int id)
    {
        var role = await _repository.GetRoleWithUsersAsync(id);
        return _mapper.Map<UserRoleWithUsersDto>(role);
    }

    public async Task<UserRoleSimpleDto> CreateAsync(UserRoleCreateDto createDto)
    {
        var role = _mapper.Map<UserRoleModel>(createDto);
        await _repository.AddAsync(role);
        return _mapper.Map<UserRoleSimpleDto>(role);
    }

    public async Task UpdateAsync(int id, UserRoleUpdateDto updateDto)
    {
        var role = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, role);
        await _repository.UpdateAsync(role);
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(role);
    }
}
