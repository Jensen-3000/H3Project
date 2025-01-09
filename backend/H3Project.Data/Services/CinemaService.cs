using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _cinemaRepository;

    public CinemaService(ICinemaRepository cinemaRepository)
    {
        _cinemaRepository = cinemaRepository;
    }

    public async Task<IEnumerable<CinemaReadDto>> GetAllCinemasAsync()
    {
        var cinemas = await _cinemaRepository.GetAllCinemasAsync();
        return cinemas.Select(MapModelToReadDto);
    }

    public async Task<CinemaReadDto> GetCinemaByIdAsync(int id)
    {
        var cinema = await _cinemaRepository.GetCinemaByIdAsync(id);
        return cinema == null ? null : MapModelToReadDto(cinema);
    }

    public async Task<IEnumerable<CinemaReadDto>> GetCinemasByMovieAsync(int movieId)
    {
        var cinemas = await _cinemaRepository.GetCinemasByMovieAsync(movieId);
        return cinemas.Select(c => new CinemaReadDto(c.Id, c.Name, c.Address));
    }

    public async Task<CinemaReadDto> CreateCinemaAsync(CinemaCreateDto cinemaCreateDto)
    {
        var cinema = MapCreateDtoToModel(cinemaCreateDto);
        await _cinemaRepository.AddCinemaAsync(cinema);
        return MapModelToReadDto(cinema);
    }

    public async Task UpdateCinemaAsync(CinemaUpdateDto cinemaUpdateDto)
    {
        var cinema = await _cinemaRepository.GetCinemaByIdAsync(cinemaUpdateDto.Id);
        if (cinema != null)
        {
            cinema.Name = cinemaUpdateDto.Name;
            cinema.Address = cinemaUpdateDto.Address;
            await _cinemaRepository.UpdateCinemaAsync(cinema);
        }
    }

    public async Task DeleteCinemaAsync(int id)
    {
        await _cinemaRepository.DeleteCinemaAsync(id);
    }

    private static CinemaReadDto MapModelToReadDto(Cinema cinema) => new(cinema.Id, cinema.Name, cinema.Address);

    private static Cinema MapCreateDtoToModel(CinemaCreateDto cinemaCreateDto) => new()
    {
        Name = cinemaCreateDto.Name,
        Address = cinemaCreateDto.Address
    };
}