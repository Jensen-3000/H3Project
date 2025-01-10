using AutoMapper;
using H3Project.Data.DTOs.Seats;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _repository;
    private readonly IMapper _mapper;

    public SeatService(ISeatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SeatSimpleDto>> GetAllAsync()
    {
        var seats = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<SeatSimpleDto>>(seats);
    }

    public async Task<SeatDetailedDto> GetByIdAsync(int id)
    {
        var seat = await _repository.GetSeatDetailsAsync(id);
        return _mapper.Map<SeatDetailedDto>(seat);
    }

    public async Task<IEnumerable<SeatSimpleDto>> GetByScreenAsync(int screenId)
    {
        var seats = await _repository.GetSeatsByScreenAsync(screenId);
        return _mapper.Map<IEnumerable<SeatSimpleDto>>(seats);
    }

    public async Task<SeatSimpleDto> CreateAsync(SeatCreateDto createDto)
    {
        var seat = _mapper.Map<SeatModel>(createDto);
        await _repository.AddAsync(seat);
        return _mapper.Map<SeatSimpleDto>(seat);
    }

    public async Task UpdateAsync(int id, SeatUpdateDto updateDto)
    {
        var seat = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, seat);
        await _repository.UpdateAsync(seat);
    }

    public async Task DeleteAsync(int id)
    {
        var seat = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(seat);
    }
}
