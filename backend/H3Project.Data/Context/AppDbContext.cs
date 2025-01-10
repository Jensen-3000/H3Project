using H3Project.Data.Extensions;
using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace H3Project.Data.Context;

public class AppDbContext : DbContext
{
    private readonly DbContextOptions _options;

    #region DbSet Properties
    public DbSet<CinemaModel> Cinemas { get; set; }
    public DbSet<GenreModel> Genres { get; set; }
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<ScreenModel> Screens { get; set; }
    public DbSet<ScreeningModel> Screenings { get; set; }
    public DbSet<SeatModel> Seats { get; set; }
    public DbSet<SeatAvailabilityModel> SeatAvailabilities { get; set; }
    public DbSet<TicketModel> Tickets { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<UserRoleModel> UserRoles { get; set; }
    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        _options = options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntities(modelBuilder);
        ConfigureRelationships(modelBuilder);
        ConfigureIndexes(modelBuilder);
        modelBuilder.SeedData();
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        // Configure Ticket price precision
        modelBuilder.Entity<TicketModel>()
            .Property(t => t.Price)
            .HasPrecision(18, 2);
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        // Configure MovieGenre composite key and relationships
        modelBuilder.Entity<MovieGenre>(mg =>
        {
            mg.HasKey(x => new { x.MovieId, x.GenreId });

            mg.HasOne(x => x.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(x => x.MovieId);

            mg.HasOne(x => x.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(x => x.GenreId);
        });

        // Configure Screen relationships
        modelBuilder.Entity<ScreenModel>()
            .HasMany(s => s.Seats)
            .WithOne(s => s.Screen)
            .HasForeignKey(s => s.ScreenId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ScreenModel>()
            .HasMany(s => s.Screenings)
            .WithOne(s => s.Screen)
            .HasForeignKey(s => s.ScreenId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure SeatAvailability relationships
        modelBuilder.Entity<SeatAvailabilityModel>()
            .HasKey(sa => new { sa.ScreeningId, sa.SeatId });

        modelBuilder.Entity<SeatAvailabilityModel>()
            .HasOne(sa => sa.Screening)
            .WithMany(s => s.SeatAvailabilities)
            .HasForeignKey(sa => sa.ScreeningId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SeatAvailabilityModel>()
            .HasOne(sa => sa.Seat)
            .WithMany(s => s.SeatAvailabilities)
            .HasForeignKey(sa => sa.SeatId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Ticket relationships
        modelBuilder.Entity<TicketModel>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TicketModel>()
            .HasOne(t => t.Screening)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ScreeningId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TicketModel>()
            .HasOne(t => t.Seat)
            .WithMany()
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    */
}