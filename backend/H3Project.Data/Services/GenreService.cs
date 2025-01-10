using AutoMapper;
using H3Project.Data.DTOs.Genres;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreSimpleDto>> GetAllAsync()
    {
        var genres = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<GenreSimpleDto>>(genres);
    }

    public async Task<GenreDetailedDto> GetByIdAsync(int id)
    {
        var genre = await _repository.GetGenreWithMoviesAsync(id);
        return _mapper.Map<GenreDetailedDto>(genre);
    }

    public async Task<GenreSimpleDto> CreateAsync(GenreCreateDto createDto)
    {
        var genre = _mapper.Map<GenreModel>(createDto);
        await _repository.AddAsync(genre);
        return _mapper.Map<GenreSimpleDto>(genre);
    }

    public async Task UpdateAsync(int id, GenreUpdateDto updateDto)
    {
        var genre = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, genre);
        await _repository.UpdateAsync(genre);
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(genre);
    }
}
