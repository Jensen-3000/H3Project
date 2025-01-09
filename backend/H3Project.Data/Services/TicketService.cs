using AutoMapper;
using H3Project.Data.DTOs.Tickets;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<List<TicketReadDto>> GetAllTicketsAsync()
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        return _mapper.Map<List<TicketReadDto>>(tickets);
    }

    public async Task<TicketReadDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        return ticket == null ? null : _mapper.Map<TicketReadDto>(ticket);
    }

    public async Task<TicketReadDto> CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        var ticket = _mapper.Map<Ticket>(ticketCreateDto);
        await _ticketRepository.AddTicketAsync(ticket);

        var createdTicket = await _ticketRepository.GetTicketByIdAsync(ticket.Id);
        return _mapper.Map<TicketReadDto>(createdTicket);
    }

    public async Task<bool> UpdateTicketAsync(int id, TicketUpdateDto ticketUpdateDto)
    {
        if (id != ticketUpdateDto.Id)
        {
            return false;
        }

        var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return false;
        }

        _mapper.Map(ticketUpdateDto, ticket);
        await _ticketRepository.UpdateTicketAsync(ticket);

        return true;
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return false;
        }

        await _ticketRepository.DeleteTicketAsync(ticket);
        return true;
    }
}