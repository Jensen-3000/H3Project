using H3Project.Data.Models;
using H3Project.Data.Utilities;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        SeedUserRoles(modelBuilder);
        SeedUsers(modelBuilder);
        SeedCinemas(modelBuilder);
        SeedScreens(modelBuilder);
        SeedGenres(modelBuilder);
        SeedMovies(modelBuilder);
        SeedMovieGenres(modelBuilder);

        var seats = SeedSeats(modelBuilder);
        var screenings = SeedScreenings(modelBuilder);

        SeedSeatAvailabilities(modelBuilder, seats, screenings);
        SeedTickets(modelBuilder);
    }

    private static void SeedUserRoles(ModelBuilder modelBuilder)
    {
        var userRoles = new[]
        {
            new UserRoleModel { Id = 1, Role = "Admin" },
            new UserRoleModel { Id = 2, Role = "User" }
        };

        modelBuilder.Entity<UserRoleModel>().HasData(userRoles);
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var users = new[]
        {
            new UserModel
            {
                Id = 1,
                Username = "a",
                Email = "admin@cinema.com",
                Password = PasswordHasher.HashPassword("123"),
                UserRoleId = 1
            },
            new UserModel
            {
                Id = 2,
                Username = "john_doe",
                Email = "john@example.com",
                Password = PasswordHasher.HashPassword("123"),
                UserRoleId = 2
            },
            new UserModel
            {
                Id = 3,
                Username = "jane_smith",
                Email = "jane@example.com",
                Password = PasswordHasher.HashPassword("1234"),
                UserRoleId = 2
            }
        };

        modelBuilder.Entity<UserModel>().HasData(users);
    }

    private static void SeedCinemas(ModelBuilder modelBuilder)
    {
        var cinemas = new[]
        {
            new CinemaModel { Id = 1, Name = "CineCity Central", Address = "123 Main St" },
            new CinemaModel { Id = 2, Name = "MoviePlex", Address = "456 Park Ave" },
            new CinemaModel { Id = 3, Name = "Star Cinema", Address = "789 Broadway" }
        };

        modelBuilder.Entity<CinemaModel>().HasData(cinemas);
    }

    private static void SeedScreens(ModelBuilder modelBuilder)
    {
        var screens = new[]
        {
            new ScreenModel { Id = 1, CinemaId = 1, Name = "Lille Sal" },
            new ScreenModel { Id = 2, CinemaId = 1, Name = "Stor Sal" },
            new ScreenModel { Id = 3, CinemaId = 2, Name = "Lille Sal" },
            new ScreenModel { Id = 4, CinemaId = 2, Name = "Stor Sal" },
            new ScreenModel { Id = 5, CinemaId = 3, Name = "Maxi Sal" }
        };

        modelBuilder.Entity<ScreenModel>().HasData(screens);
    }

    private static List<SeatModel> SeedSeats(ModelBuilder modelBuilder)
    {
        var allSeats = new List<SeatModel>();
        var seatId = 1;
        foreach (var screenId in new[] { 1, 2, 3, 4, 5 })
        {
            var screenSeats = SeatCreator.CreateSeats(
                screenId,
                ["A", "B", "C", "D"],
                6,
                seatId
            );
            allSeats.AddRange(screenSeats);
            seatId += screenSeats.Count;
        }

        modelBuilder.Entity<SeatModel>().HasData(allSeats);
        return allSeats;
    }

    private static void SeedGenres(ModelBuilder modelBuilder)
    {
        var genres = new[]
        {
            new GenreModel { Id = 1, Name = "Action" },
            new GenreModel { Id = 2, Name = "Comedy" },
            new GenreModel { Id = 3, Name = "Drama" },
            new GenreModel { Id = 4, Name = "Sci-Fi" },
            new GenreModel { Id = 5, Name = "Horror" },
            new GenreModel { Id = 6, Name = "Romance" }
        };

        modelBuilder.Entity<GenreModel>().HasData(genres);
    }

    private static void SeedMovies(ModelBuilder modelBuilder)
    {
        var movies = new[]
        {
            new MovieModel
            {
                Id = 1,
                Title = "Space Warriors",
                Description = "Epic space adventure",
                ImageUrl = "https://placehold.co/400x600?text=Space+Warriors",
                Slug = "space-warriors",
                Duration = new TimeSpan(2, 15, 0)
            },
            new MovieModel
            {
                Id = 2,
                Title = "Love & Laughs",
                Description = "Romantic comedy",
                ImageUrl = "https://placehold.co/400x600?text=Love+and+Laughs",
                Slug = "love-and-laughs",
                Duration = new TimeSpan(1, 45, 0)
            },
            new MovieModel
            {
                Id = 3,
                Title = "Night Terror",
                Description = "Horror thriller",
                ImageUrl = "https://placehold.co/400x600?text=Night+Terror",
                Slug = "night-terror",
                Duration = new TimeSpan(1, 50, 0)
            },
            new MovieModel
            {
                Id = 4,
                Title = "The Last Stand",
                Description = "Action drama",
                ImageUrl = "https://placehold.co/400x600?text=Last+Stand",
                Slug = "the-last-stand",
                Duration = new TimeSpan(2, 30, 0)
            }
        };

        modelBuilder.Entity<MovieModel>().HasData(movies);
    }

    private static void SeedMovieGenres(ModelBuilder modelBuilder)
    {
        var movieGenres = new[]
        {
            new MovieGenre { MovieId = 1, GenreId = 1 },
            new MovieGenre { MovieId = 1, GenreId = 4 },
            new MovieGenre { MovieId = 2, GenreId = 2 },
            new MovieGenre { MovieId = 2, GenreId = 6 },
            new MovieGenre { MovieId = 3, GenreId = 5 },
            new MovieGenre { MovieId = 4, GenreId = 1 },
            new MovieGenre { MovieId = 4, GenreId = 3 }
        };

        modelBuilder.Entity<MovieGenre>().HasData(movieGenres);
    }

    private static List<ScreeningModel> SeedScreenings(ModelBuilder modelBuilder)
    {
        var screenings = new List<ScreeningModel>();
        var screeningId = 1;
        var now = DateTime.Now.Date;

        for (var day = 1; day <= 7; day++)
            screenings.AddRange(new[]
            {
                new ScreeningModel
                {
                    Id = screeningId++,
                    StartTime = now.AddDays(day).AddHours(14),
                    ScreenId = 1,
                    MovieId = 1
                },
                new ScreeningModel
                {
                    Id = screeningId++,
                    StartTime = now.AddDays(day).AddHours(17),
                    ScreenId = 2,
                    MovieId = 2
                },
                new ScreeningModel
                {
                    Id = screeningId++,
                    StartTime = now.AddDays(day).AddHours(20),
                    ScreenId = 3,
                    MovieId = 3
                }
            });

        modelBuilder.Entity<ScreeningModel>().HasData(screenings);
        return screenings;
    }

    private static void SeedSeatAvailabilities(ModelBuilder modelBuilder, List<SeatModel> allSeats,
        List<ScreeningModel> screenings)
    {
        int[] seatsTaken = { 1, 2, 25 };
        var seatAvailabilities = new List<SeatAvailabilityModel>();

        foreach (var screening in screenings)
        {
            var screenSeats = allSeats.Where(s => s.ScreenId == screening.ScreenId);
            foreach (var seat in screenSeats)
                seatAvailabilities.Add(new SeatAvailabilityModel
                {
                    ScreeningId = screening.Id,
                    SeatId = seat.Id,
                    IsAvailable = !seatsTaken.Contains(seat.Id)
                });
        }

        modelBuilder.Entity<SeatAvailabilityModel>().HasData(seatAvailabilities);
    }

    private static void SeedTickets(ModelBuilder modelBuilder)
    {
        int[] seatsTaken = [1, 2, 25];

        var tickets = new[]
        {
            new TicketModel
            {
                Id = 1,
                Price = 12.99m,
                UserId = 2, // John
                ScreeningId = 1,
                SeatId = seatsTaken[0]
            },
            new TicketModel
            {
                Id = 2,
                Price = 12.99m,
                UserId = 2, // John
                ScreeningId = 1,
                SeatId = seatsTaken[1]
            },
            new TicketModel
            {
                Id = 3,
                Price = 14.99m,
                UserId = 3, // Jane
                ScreeningId = 2,
                SeatId = seatsTaken[2]
            }
        };
        modelBuilder.Entity<TicketModel>().HasData(tickets);
    }
}
