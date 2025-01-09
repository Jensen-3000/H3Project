using H3Project.Data.Models;
using H3Project.Data.Utilities;
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
            new Movie
            {
                Id = 1,
                Title = "Fast & Furious 10",
                Slug = "fast-and-furious-10",
                Description = "High-speed action",
                ReleaseDate = new DateOnly(2024, 1, 15),
                Duration = new TimeSpan(2, 30, 0),
                ImageUrl = "https://placehold.co/400x600?text=Fast+And+Furious+10"
            },
            new Movie
            {
                Id = 2,
                Title = "Laugh Out Loud",
                Slug = "laugh-out-loud",
                Description = "Hilarious comedy",
                ReleaseDate = new DateOnly(2024, 2, 10),
                Duration = new TimeSpan(1, 45, 0),
                ImageUrl = "https://placehold.co/400x600?text=Laugh+Out+Loud"
            },
            new Movie
            {
                Id = 3,
                Title = "Deep Emotions",
                Slug = "deep-emotions",
                Description = "Heartfelt drama",
                ReleaseDate = new DateOnly(2024, 3, 5),
                Duration = new TimeSpan(2, 10, 0),
                ImageUrl = "https://placehold.co/400x600?text=Deep+Emotions"
            }
        );

        // Seed join table for Movies and Genres
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.HasData(
                new { MoviesId = 1, GenresId = 1 }, // Fast & Furious 10 is Action
                new { MoviesId = 1, GenresId = 2 }, // Fast & Furious 10 is Comedy
                new { MoviesId = 2, GenresId = 2 }, // Laugh Out Loud is Comedy
                new { MoviesId = 3, GenresId = 3 } // Deep Emotions is Drama
            ));

        // Seed Schedules
        modelBuilder.Entity<Schedule>().HasData(
            new Schedule
            {
                Id = 1,
                MovieId = 1,
                TheaterId = 1,
                ShowTime = new DateTime(2024, 1, 16, 18, 0, 0),
                EndTime = new DateTime(2024, 1, 16, 20, 30, 0),
                BasePrice = 12.99m
            },
            new Schedule
            {
                Id = 2,
                MovieId = 1,
                TheaterId = 1,
                ShowTime = new DateTime(2024, 1, 16, 21, 0, 0),
                EndTime = new DateTime(2024, 1, 16, 23, 30, 0),
                BasePrice = 14.99m // Evening show premium
            },
            new Schedule
            {
                Id = 3,
                MovieId = 2,
                TheaterId = 2,
                ShowTime = new DateTime(2024, 2, 11, 15, 0, 0),
                EndTime = new DateTime(2024, 2, 11, 16, 45, 0),
                BasePrice = 9.99m // Matinee discount
            }
        );

        // Generate seats for all theaters
        var allSeats = new List<Seat>();
        allSeats.AddRange(SeatGenerator.GenerateTheaterSeats(1, 1, 8, 12)); // Theater 1: 8 rows, 12 seats each
        allSeats.AddRange(SeatGenerator.GenerateTheaterSeats(2, 97, 6, 10)); // Theater 2: 6 rows, 10 seats each
        allSeats.AddRange(SeatGenerator.GenerateTheaterSeats(3, 157, 5, 8)); // Theater 3: 5 rows, 8 seats each

        modelBuilder.Entity<Seat>().HasData(allSeats);


        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "john_doe",
                Email = "john@example.com",
                PasswordHash = "hashed_password_1",
                UserRoleId = 1
            },
            new User
            {
                Id = 2,
                Username = "jane_smith",
                Email = "jane@example.com",
                PasswordHash = "hashed_password_2",
                UserRoleId = 2
            },
            new User
            {
                Id = 3,
                Username = "alice_jones",
                Email = "alice@example.com",
                PasswordHash = "hashed_password_3",
                UserRoleId = 2
            }
        );

        // Seed UserRoles
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, Role = "Admin" },
            new UserRole { Id = 2, Role = "Customer" }
        );

        // Seed Tickets
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                ScheduleId = 1,
                SeatId = 1,
                UserId = 2,
                PurchaseDate = DateTime.Now.AddDays(-5),
                Price = 12.99m
            },
            new Ticket
            {
                Id = 2,
                ScheduleId = 2,
                SeatId = 98,
                UserId = 3,
                PurchaseDate = DateTime.Now.AddDays(-3),
                Price = 14.99m
            },
            new Ticket
            {
                Id = 3,
                ScheduleId = 3,
                SeatId = 157,
                UserId = 2,
                PurchaseDate = DateTime.Now.AddDays(-1),
                Price = 9.99m
            }
        );
    }
}