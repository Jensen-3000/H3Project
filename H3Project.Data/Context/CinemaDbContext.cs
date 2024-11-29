using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Context;

public class CinemaDbContext : DbContext
{
    private readonly DbContextOptions _options;
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<CinemaDetail> CinemaDetails { get; set; }

    public CinemaDbContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }
}