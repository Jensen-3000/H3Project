using H3Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Seed Cinemas
        modelBuilder.Entity<Cinema>().HasData(
            new Cinema { Id = 1, Name = "Grand Cinema", Address = "123 Movie St" },
            new Cinema { Id = 2, Name = "Downtown Cinema", Address = "456 Central Ave" }
        );

        // Seed Theaters
        modelBuilder.Entity<Theater>().HasData(
            new Theater { Id = 1, Name = "Screen 1", CinemaId = 1 },
            new Theater { Id = 2, Name = "Screen 2", CinemaId = 1 },
            new Theater { Id = 3, Name = "Screen 1", CinemaId = 2 }
        );

        // Seed Genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Drama" }
        );

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Fast & Furious 10", Description = "High-speed action", ReleaseDate = new DateOnly(2024, 1, 15), Duration = new TimeSpan(2, 30, 0) },
            new Movie { Id = 2, Title = "Laugh Out Loud", Description = "Hilarious comedy", ReleaseDate = new DateOnly(2024, 2, 10), Duration = new TimeSpan(1, 45, 0) },
            new Movie { Id = 3, Title = "Deep Emotions", Description = "Heartfelt drama", ReleaseDate = new DateOnly(2024, 3, 5), Duration = new TimeSpan(2, 10, 0) }
        );


        // Seed join table for Movies and Genres
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.HasData(
                new { MoviesId = 1, GenresId = 1 }, // Fast & Furious 10 is Action
                new { MoviesId = 1, GenresId = 2 }, // Fast & Furious 10 is Comedy
                new { MoviesId = 2, GenresId = 2 }, // Laugh Out Loud is Comedy
                new { MoviesId = 3, GenresId = 3 }  // Deep Emotions is Drama
            ));

        // Seed Schedules
        modelBuilder.Entity<Schedule>().HasData(
            new Schedule { Id = 1, MovieId = 1, TheaterId = 1, ShowTime = new DateTime(2024, 1, 16, 18, 0, 0), EndTime = new DateTime(2024, 1, 16, 20, 30, 0) },
            new Schedule { Id = 2, MovieId = 2, TheaterId = 2, ShowTime = new DateTime(2024, 2, 11, 15, 0, 0), EndTime = new DateTime(2024, 2, 11, 16, 45, 0) },
            new Schedule { Id = 3, MovieId = 3, TheaterId = 3, ShowTime = new DateTime(2024, 3, 6, 20, 0, 0), EndTime = new DateTime(2024, 3, 6, 22, 10, 0) }
        );

        // Seed Seats
        modelBuilder.Entity<Seat>().HasData(
            new Seat { Id = 1, Row = "A", Number = 1, IsAvailable = true, TheaterId = 1 },
            new Seat { Id = 2, Row = "A", Number = 2, IsAvailable = true, TheaterId = 1 },
            new Seat { Id = 3, Row = "B", Number = 3, IsAvailable = false, TheaterId = 2 },
            new Seat { Id = 4, Row = "C", Number = 4, IsAvailable = true, TheaterId = 3 }
        );

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "john_doe", Email = "john@example.com", PasswordHash = "hashed_password_1", UserType = "Customer" },
            new User { Id = 2, Username = "jane_smith", Email = "jane@example.com", PasswordHash = "hashed_password_2", UserType = "Admin" }
        );

        // Seed Tickets
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { Id = 1, ScheduleId = 1, SeatId = 1, UserId = 1, PurchaseDate = new DateTime(2024, 1, 15, 14, 0, 0), Price = 10.99m },
            new Ticket { Id = 2, ScheduleId = 2, SeatId = 3, UserId = 2, PurchaseDate = new DateTime(2024, 2, 10, 10, 0, 0), Price = 8.50m }
        );
    }
}