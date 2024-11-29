using H3Project.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Showtime)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithMany()
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Booking)
            .WithMany(b => b.Tickets)
            .HasForeignKey(t => t.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data
        modelBuilder.Entity<Cinema>().HasData(
            new Cinema { CinemaId = 1, Name = "Cinema One" },
            new Cinema { CinemaId = 2, Name = "Cinema Two" }
        );

        modelBuilder.Entity<Screen>().HasData(
            new Screen { ScreenId = 1, Name = "Screen One", CinemaId = 1 },
            new Screen { ScreenId = 2, Name = "Screen Two", CinemaId = 2 }
        );

        modelBuilder.Entity<Seat>().HasData(
            new Seat { SeatId = 1, SeatNumber = "A1", ScreenId = 1 },
            new Seat { SeatId = 2, SeatNumber = "A2", ScreenId = 1 }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { MovieId = 1, Title = "Movie One" },
            new Movie { MovieId = 2, Title = "Movie Two" }
        );

        modelBuilder.Entity<Showtime>().HasData(
            new Showtime { ShowtimeId = 1, StartTime = DateTime.Now, ScreenId = 1, MovieId = 1 },
            new Showtime { ShowtimeId = 2, StartTime = DateTime.Now.AddHours(2), ScreenId = 2, MovieId = 2 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, Name = "John Doe" },
            new Customer { CustomerId = 2, Name = "Jane Doe" }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking { BookingId = 1, BookingDate = DateTime.Now, CustomerId = 1 },
            new Booking { BookingId = 2, BookingDate = DateTime.Now.AddDays(1), CustomerId = 2 }
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { TicketId = 1, ShowtimeId = 1, SeatId = 1, BookingId = 1 },
            new Ticket { TicketId = 2, ShowtimeId = 2, SeatId = 2, BookingId = 2 }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, Name = "Action" },
            new Genre { GenreId = 2, Name = "Comedy" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, Name = "Manager" },
            new Role { RoleId = 2, Name = "Staff" }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, Name = "Alice", CinemaId = 1, RoleId = 1 },
            new Employee { EmployeeId = 2, Name = "Bob", CinemaId = 2, RoleId = 2 },
            new Employee { EmployeeId = 3, Name = "Charlie", CinemaId = 1, RoleId = 2 },
            new Employee { EmployeeId = 4, Name = "David", CinemaId = 2, RoleId = 1 }
        );

        modelBuilder.Entity<CinemaDetail>().HasData(
            new CinemaDetail { CinemaDetailId = 1, Address = "123 Main St", PhoneNumber = "123-456-7890", CinemaId = 1 },
            new CinemaDetail { CinemaDetailId = 2, Address = "456 Elm St", PhoneNumber = "987-654-3210", CinemaId = 2 }
        );
    }
}
