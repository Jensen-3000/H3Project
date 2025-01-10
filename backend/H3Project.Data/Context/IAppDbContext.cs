using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace H3Project.Data.Context;

public interface IAppDbContext
{
    DbSet<CinemaModel> Cinemas { get; set; }
    DbSet<GenreModel> Genres { get; set; }
    DbSet<MovieModel> Movies { get; set; }
    DbSet<MovieGenre> MovieGenres { get; set; }
    DbSet<ScreenModel> Screens { get; set; }
    DbSet<ScreeningModel> Screenings { get; set; }
    DbSet<SeatModel> Seats { get; set; }
    DbSet<SeatAvailabilityModel> SeatAvailabilities { get; set; }
    DbSet<TicketModel> Tickets { get; set; }
    DbSet<UserModel> Users { get; set; }
    DbSet<UserRoleModel> UserRoles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
}