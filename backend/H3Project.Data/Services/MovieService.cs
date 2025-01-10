using AutoMapper;
using H3Project.Data.DTOs.Movies;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovieSimpleDto>> GetAllAsync()
    {
        var movies = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MovieSimpleDto>>(movies);
    }

    public async Task<MovieDetailedDto> GetByIdAsync(int id)
    {
        var movie = await _repository.GetMovieWithDetailsAsync(id);
        return _mapper.Map<MovieDetailedDto>(movie);
    }

    public async Task<MovieDetailedDto> GetBySlugAsync(string slug)
    {
        var movie = await _repository.GetMovieBySlugAsync(slug);
        return _mapper.Map<MovieDetailedDto>(movie);
    }

    public async Task<MovieSimpleDto> CreateAsync(MovieCreateDto createDto)
    {
        var movie = _mapper.Map<MovieModel>(createDto);
        await _repository.AddAsync(movie);
        await _repository.UpdateMovieGenresAsync(movie, createDto.GenreIds);
        return _mapper.Map<MovieSimpleDto>(movie);
    }

    public async Task UpdateAsync(int id, MovieUpdateDto updateDto)
    {
        var movie = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, movie);
        await _repository.UpdateAsync(movie);
        await _repository.UpdateMovieGenresAsync(movie, updateDto.GenreIds);
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(movie);
    }
}