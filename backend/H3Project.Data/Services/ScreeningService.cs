using AutoMapper;
using H3Project.Data.DTOs.Screenings;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class ScreeningService : IScreeningService
{
    private readonly IScreeningRepository _repository;
    private readonly IMapper _mapper;

    public ScreeningService(IScreeningRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ScreeningSimpleDto>> GetAllAsync()
    {
        var screenings = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ScreeningSimpleDto>>(screenings);
    }

    public async Task<ScreeningAvailableSeatsDto> GetByIdAsync(int id)
    {
        var screening = await _repository.GetScreeningWithDetailsAsync(id);
        return _mapper.Map<ScreeningAvailableSeatsDto>(screening);
    }

    public async Task<IEnumerable<ScreeningSimpleDto>> GetByMovieAsync(int movieId)
    {
        var screenings = await _repository.GetScreeningsByMovieAsync(movieId);
        return _mapper.Map<IEnumerable<ScreeningSimpleDto>>(screenings);
    }

    public async Task<IEnumerable<ScreeningSimpleDto>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        var screenings = await _repository.GetScreeningsByDateRangeAsync(start, end);
        return _mapper.Map<IEnumerable<ScreeningSimpleDto>>(screenings);
    }

    public async Task<ScreeningSimpleDto> CreateAsync(ScreeningCreateDto createDto)
    {
        var screening = _mapper.Map<ScreeningModel>(createDto);
        await _repository.AddAsync(screening);
        return _mapper.Map<ScreeningSimpleDto>(screening);
    }

    public async Task UpdateAsync(int id, ScreeningUpdateDto updateDto)
    {
        var screening = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, screening);
        await _repository.UpdateAsync(screening);
    }

    public async Task DeleteAsync(int id)
    {
        var screening = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(screening);
    }
}
