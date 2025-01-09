using AutoMapper;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class TheaterService : ITheaterService
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly IMapper _mapper;

    public TheaterService(ITheaterRepository theaterRepository, IMapper mapper)
    {
        _theaterRepository = theaterRepository;
        _mapper = mapper;
    }

    public async Task<List<TheaterReadDto>> GetAllTheatersAsync()
    {
        var theaters = await _theaterRepository.GetAllTheatersAsync();
        return _mapper.Map<List<TheaterReadDto>>(theaters);
    }

    public async Task<TheaterReadDto?> GetTheaterByIdAsync(int id)
    {
        var theater = await _theaterRepository.GetTheaterByIdAsync(id);
        return theater == null ? null : _mapper.Map<TheaterReadDto>(theater);
    }

    public async Task<TheaterReadDto> CreateTheaterAsync(TheaterCreateDto theaterCreateDto)
    {
        var theater = _mapper.Map<Theater>(theaterCreateDto);
        await _theaterRepository.AddTheaterAsync(theater);
        return _mapper.Map<TheaterReadDto>(theater);
    }

    public async Task<bool> UpdateTheaterAsync(int id, TheaterUpdateDto theaterUpdateDto)
    {
        if (id != theaterUpdateDto.Id)
        {
            return false;
        }

        var theater = await _theaterRepository.GetTheaterByIdAsync(id);
        if (theater == null)
        {
            return false;
        }

        _mapper.Map(theaterUpdateDto, theater);
        await _theaterRepository.UpdateTheaterAsync(theater);

        return true;
    }

    public async Task<bool> DeleteTheaterAsync(int id)
    {
        var theater = await _theaterRepository.GetTheaterByIdAsync(id);
        if (theater == null)
        {
            return false;
        }

        await _theaterRepository.DeleteTheaterAsync(theater);
        return true;
    }
}