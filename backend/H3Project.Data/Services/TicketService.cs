using AutoMapper;
using H3Project.Data.DTOs.Tickets;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TicketSimpleDto>> GetAllAsync()
    {
        var tickets = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TicketSimpleDto>>(tickets);
    }

    public async Task<TicketWithScreeningAndSeatDto> GetByIdAsync(int id)
    {
        var ticket = await _repository.GetTicketWithDetailsAsync(id);
        return _mapper.Map<TicketWithScreeningAndSeatDto>(ticket);
    }

    public async Task<IEnumerable<TicketSimpleDto>> GetByUserAsync(int userId)
    {
        var tickets = await _repository.GetTicketsByUserAsync(userId);
        return _mapper.Map<IEnumerable<TicketSimpleDto>>(tickets);
    }

    public async Task<IEnumerable<TicketSimpleDto>> GetByScreeningAsync(int screeningId)
    {
        var tickets = await _repository.GetTicketsByScreeningAsync(screeningId);
        return _mapper.Map<IEnumerable<TicketSimpleDto>>(tickets);
    }

    public async Task<TicketSimpleDto> CreateAsync(TicketCreateDto createDto)
    {
        var ticket = _mapper.Map<TicketModel>(createDto);
        await _repository.AddAsync(ticket);
        return _mapper.Map<TicketSimpleDto>(ticket);
    }

    public async Task DeleteAsync(int id)
    {
        var ticket = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(ticket);
    }
}
