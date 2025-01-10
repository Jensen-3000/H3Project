using AutoMapper;
using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _repository;
    private readonly IMapper _mapper;

    public CinemaService(ICinemaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CinemaSimpleDto>> GetAllAsync()
    {
        var cinemas = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaSimpleDto>>(cinemas);
    }

    public async Task<CinemaDetailedDto> GetByIdAsync(int id)
    {
        var cinema = await _repository.GetCinemaWithScreensAsync(id);
        return _mapper.Map<CinemaDetailedDto>(cinema);
    }

    public async Task<CinemaSimpleDto> CreateAsync(CinemaCreateDto createDto)
    {
        var cinema = _mapper.Map<CinemaModel>(createDto);
        await _repository.AddAsync(cinema);
        return _mapper.Map<CinemaSimpleDto>(cinema);
    }

    public async Task UpdateAsync(int id, CinemaUpdateDto updateDto)
    {
        var cinema = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, cinema);
        await _repository.UpdateAsync(cinema);
    }

    public async Task DeleteAsync(int id)
    {
        var cinema = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(cinema);
    }
}
