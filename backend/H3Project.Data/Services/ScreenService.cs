using AutoMapper;
using H3Project.Data.DTOs.Screens;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _repository;
    private readonly IMapper _mapper;

    public ScreenService(IScreenRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ScreenSimpleDto>> GetAllAsync()
    {
        var screens = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ScreenSimpleDto>>(screens);
    }

    public async Task<ScreenDetailedDto> GetByIdAsync(int id)
    {
        var screen = await _repository.GetScreenWithDetailsAsync(id);
        return _mapper.Map<ScreenDetailedDto>(screen);
    }

    public async Task<IEnumerable<ScreenSimpleDto>> GetByCinemaAsync(int cinemaId)
    {
        var screens = await _repository.GetScreensByCinemaAsync(cinemaId);
        return _mapper.Map<IEnumerable<ScreenSimpleDto>>(screens);
    }

    public async Task<ScreenSimpleDto> CreateAsync(ScreenCreateDto createDto)
    {
        var screen = _mapper.Map<ScreenModel>(createDto);
        await _repository.AddAsync(screen);
        return _mapper.Map<ScreenSimpleDto>(screen);
    }

    public async Task UpdateAsync(int id, ScreenUpdateDto updateDto)
    {
        var screen = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, screen);
        await _repository.UpdateAsync(screen);
    }

    public async Task DeleteAsync(int id)
    {
        var screen = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(screen);
    }
}
