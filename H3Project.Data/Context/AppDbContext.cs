using H3Project.Data.Extensions;
using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Context;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly DbContextOptions _options;

    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Set Price decimal precision
        var pricePrecision = (Precision: 18, Scale: 2);

        modelBuilder.Entity<Ticket>()
            .Property(t => t.Price)
            .HasPrecision(pricePrecision.Precision, pricePrecision.Scale);

        // Handle FK Cascade Delete for Ticket
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithMany()
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data 
        modelBuilder.SeedData();
    }
}
