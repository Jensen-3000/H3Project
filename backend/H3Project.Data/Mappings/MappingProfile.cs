using AutoMapper;
using H3Project.Data.DTOs.Cinemas;
using H3Project.Data.DTOs.Genres;
using H3Project.Data.DTOs.Movies;
using H3Project.Data.DTOs.Screenings;
using H3Project.Data.DTOs.Screens;
using H3Project.Data.DTOs.SeatAvailabilities;
using H3Project.Data.DTOs.Seats;
using H3Project.Data.DTOs.Tickets;
using H3Project.Data.DTOs.UserRoles;
using H3Project.Data.DTOs.Users;
using H3Project.Data.Models;

namespace H3Project.Data.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cinema
        CreateMap<CinemaModel, CinemaSimpleDto>();
        CreateMap<CinemaModel, CinemaDetailedDto>();

        // Genre
        CreateMap<GenreModel, GenreSimpleDto>();
        CreateMap<GenreModel, GenreDetailedDto>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src =>
                src.MovieGenres.Select(mg => mg.Movie)));

        // Movie
        CreateMap<MovieModel, MovieSimpleDto>();
        CreateMap<MovieModel, MovieDetailedDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                src.MovieGenres.Select(mg => mg.Genre)));

        // Screen
        CreateMap<ScreenModel, ScreenSimpleDto>();
        CreateMap<ScreenModel, ScreenDetailedDto>();

        // Screening
        CreateMap<ScreeningModel, ScreeningSimpleDto>();
        CreateMap<ScreeningModel, ScreeningDetailsDto>();
        CreateMap<ScreeningModel, ScreeningAvailableSeatsDto>();

        // Seat
        CreateMap<SeatModel, SeatSimpleDto>();
        CreateMap<SeatModel, SeatDetailedDto>();

        // SeatAvailability
        CreateMap<SeatAvailabilityModel, SeatAvailabilitySimpleDto>();
        CreateMap<SeatAvailabilityModel, SeatAvailabilityDetailedDto>();

        // Ticket
        CreateMap<TicketModel, TicketSimpleDto>();
        CreateMap<TicketModel, TicketWithScreeningAndSeatDto>();

        // User
        CreateMap<UserModel, UserSimpleDto>();
        CreateMap<UserModel, UserWithRoleAndTicketsDto>();
        CreateMap<UserModel, UserCreateDto>().ReverseMap();
        CreateMap<UserModel, UserUpdateDto>();

        // UserRole
        CreateMap<UserRoleModel, UserRoleSimpleDto>();
        CreateMap<UserRoleModel, UserRoleWithUsersDto>();
    }
}
