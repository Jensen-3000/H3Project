using AutoMapper;
using H3Project.Data.DTOs.Schedules;
using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Theaters;
using H3Project.Data.DTOs.Tickets;
using H3Project.Data.DTOs.UserRoles;
using H3Project.Data.DTOs.Users;
using H3Project.Data.Models;

namespace H3Project.Data.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole))
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets));
        CreateMap<User, UserReadDtoSimple>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole));
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<UserRole, UserRoleDto>();
        CreateMap<UserRoleDto, UserRole>();
        CreateMap<Ticket, TicketReadDto>()
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat));
        CreateMap<TicketCreateDto, Ticket>();
        CreateMap<TicketUpdateDto, Ticket>();
        CreateMap<Schedule, ScheduleReadDto>()
            .ForMember(dest => dest.TheaterName, opt => opt.MapFrom(src => src.Theater.Name))
            .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));
        CreateMap<ScheduleCreateDto, Schedule>();
        CreateMap<ScheduleUpdateDto, Schedule>();
        CreateMap<Seat, SeatReadDto>();
        CreateMap<SeatCreateDto, Seat>();
        CreateMap<SeatUpdateDto, Seat>();
        CreateMap<Theater, TheaterReadDto>();
        CreateMap<TheaterCreateDto, Theater>();
        CreateMap<TheaterUpdateDto, Theater>();
    }
}
