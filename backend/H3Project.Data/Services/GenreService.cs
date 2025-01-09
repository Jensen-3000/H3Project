using H3Project.Data.DTOs.Genres;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<List<GenreReadDto>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllGenresAsync();
        return genres.Select(MapModelToReadDto).ToList();
    }

    public async Task<GenreReadDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(id);
        return genre == null ? null : MapModelToReadDto(genre);
    }

    public async Task<GenreReadDto> CreateGenreAsync(GenreCreateDto genreCreateDto)
    {
        var genre = MapCreateDtoToModel(genreCreateDto);
        await _genreRepository.AddGenreAsync(genre);
        return MapModelToReadDto(genre);
    }

    public async Task<bool> UpdateGenreAsync(int id, GenreUpdateDto genreUpdateDto)
    {
        if (id != genreUpdateDto.Id)
        {
            return false;
        }

        var genre = await _genreRepository.GetGenreByIdAsync(id);
        if (genre == null)
        {
            return false;
        }

        genre.Name = genreUpdateDto.Name;
        await _genreRepository.UpdateGenreAsync(genre);
        return true;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(id);
        if (genre == null)
        {
            return false;
        }

        await _genreRepository.DeleteGenreAsync(genre);
        return true;
    }

    private static GenreReadDto MapModelToReadDto(Genre genre) => new(genre.Id, genre.Name);

    private static Genre MapCreateDtoToModel(GenreCreateDto genreCreateDto) => new() { Name = genreCreateDto.Name };
}