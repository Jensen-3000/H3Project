using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace H3Project.Data.Context;

public interface IAppDbContext
{
    DbSet<Cinema> Cinemas { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Movie> Movies { get; set; }
    DbSet<Schedule> Schedules { get; set; }
    DbSet<Seat> Seats { get; set; }
    DbSet<Theater> Theaters { get; set; }
    DbSet<Ticket> Tickets { get; set; }
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
}