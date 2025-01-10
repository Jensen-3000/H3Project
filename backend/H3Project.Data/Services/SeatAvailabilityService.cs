using AutoMapper;
using H3Project.Data.DTOs.SeatAvailabilities;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class SeatAvailabilityService : ISeatAvailabilityService
{
    private readonly ISeatAvailabilityRepository _repository;
    private readonly IMapper _mapper;

    public SeatAvailabilityService(ISeatAvailabilityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SeatAvailabilitySimpleDto>> GetByScreeningAsync(int screeningId)
    {
        var availabilities = await _repository.GetByScreeningAsync(screeningId);
        return _mapper.Map<IEnumerable<SeatAvailabilitySimpleDto>>(availabilities);
    }

    public async Task<SeatAvailabilityDetailedDto> GetByScreeningAndSeatAsync(int screeningId, int seatId)
    {
        var availability = await _repository.GetByScreeningAndSeatAsync(screeningId, seatId);
        return _mapper.Map<SeatAvailabilityDetailedDto>(availability);
    }

    public async Task<bool> ToggleAvailabilityAsync(SeatAvailabilityToggleDto toggleDto)
        => await _repository.ToggleAvailabilityAsync(toggleDto.ScreeningId, toggleDto.SeatId);
}
