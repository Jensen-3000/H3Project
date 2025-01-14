using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace H3Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screens_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Screenings_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatAvailabilities",
                columns: table => new
                {
                    ScreeningId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAvailabilities", x => new { x.ScreeningId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_SeatAvailabilities_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeatAvailabilities_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScreeningId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main St", "CineCity Central" },
                    { 2, "456 Park Ave", "MoviePlex" },
                    { 3, "789 Broadway", "Star Cinema" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Sci-Fi" },
                    { 5, "Horror" },
                    { 6, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "Epic space adventure", new TimeSpan(0, 2, 15, 0, 0), "https://placehold.co/400x600?text=Space+Warriors", "Space Warriors", "space-warriors" },
                    { 2, "Romantic comedy", new TimeSpan(0, 1, 45, 0, 0), "https://placehold.co/400x600?text=Love+and+Laughs", "Love & Laughs", "love-and-laughs" },
                    { 3, "Horror thriller", new TimeSpan(0, 1, 50, 0, 0), "https://placehold.co/400x600?text=Night+Terror", "Night Terror", "night-terror" },
                    { 4, "Action drama", new TimeSpan(0, 2, 30, 0, 0), "https://placehold.co/400x600?text=Last+Stand", "The Last Stand", "the-last-stand" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 2, 2 },
                    { 6, 2 },
                    { 5, 3 },
                    { 1, 4 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Screens",
                columns: new[] { "Id", "CinemaId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Lille Sal" },
                    { 2, 1, "Stor Sal" },
                    { 3, 2, "Lille Sal" },
                    { 4, 2, "Stor Sal" },
                    { 5, 3, "Maxi Sal" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserRoleId", "Username" },
                values: new object[,]
                {
                    { 1, "admin@cinema.com", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", 1, "a" },
                    { 2, "john@example.com", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", 2, "john_doe" },
                    { 3, "jane@example.com", "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=", 2, "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "MovieId", "ScreenId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 1, 15, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 2, 2, new DateTime(2025, 1, 15, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 3, 3, new DateTime(2025, 1, 15, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 1, 1, new DateTime(2025, 1, 16, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 2, 2, new DateTime(2025, 1, 16, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 6, 3, 3, new DateTime(2025, 1, 16, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 7, 1, 1, new DateTime(2025, 1, 17, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 8, 2, 2, new DateTime(2025, 1, 17, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 9, 3, 3, new DateTime(2025, 1, 17, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 10, 1, 1, new DateTime(2025, 1, 18, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 11, 2, 2, new DateTime(2025, 1, 18, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 12, 3, 3, new DateTime(2025, 1, 18, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 13, 1, 1, new DateTime(2025, 1, 19, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 14, 2, 2, new DateTime(2025, 1, 19, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 15, 3, 3, new DateTime(2025, 1, 19, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 16, 1, 1, new DateTime(2025, 1, 20, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 17, 2, 2, new DateTime(2025, 1, 20, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 18, 3, 3, new DateTime(2025, 1, 20, 20, 0, 0, 0, DateTimeKind.Local) },
                    { 19, 1, 1, new DateTime(2025, 1, 21, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 20, 2, 2, new DateTime(2025, 1, 21, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 21, 3, 3, new DateTime(2025, 1, 21, 20, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Number", "Row", "ScreenId" },
                values: new object[,]
                {
                    { 1, 1, "A", 1 },
                    { 2, 2, "A", 1 },
                    { 3, 3, "A", 1 },
                    { 4, 4, "A", 1 },
                    { 5, 5, "A", 1 },
                    { 6, 6, "A", 1 },
                    { 7, 1, "B", 1 },
                    { 8, 2, "B", 1 },
                    { 9, 3, "B", 1 },
                    { 10, 4, "B", 1 },
                    { 11, 5, "B", 1 },
                    { 12, 6, "B", 1 },
                    { 13, 1, "C", 1 },
                    { 14, 2, "C", 1 },
                    { 15, 3, "C", 1 },
                    { 16, 4, "C", 1 },
                    { 17, 5, "C", 1 },
                    { 18, 6, "C", 1 },
                    { 19, 1, "D", 1 },
                    { 20, 2, "D", 1 },
                    { 21, 3, "D", 1 },
                    { 22, 4, "D", 1 },
                    { 23, 5, "D", 1 },
                    { 24, 6, "D", 1 },
                    { 25, 1, "A", 2 },
                    { 26, 2, "A", 2 },
                    { 27, 3, "A", 2 },
                    { 28, 4, "A", 2 },
                    { 29, 5, "A", 2 },
                    { 30, 6, "A", 2 },
                    { 31, 1, "B", 2 },
                    { 32, 2, "B", 2 },
                    { 33, 3, "B", 2 },
                    { 34, 4, "B", 2 },
                    { 35, 5, "B", 2 },
                    { 36, 6, "B", 2 },
                    { 37, 1, "C", 2 },
                    { 38, 2, "C", 2 },
                    { 39, 3, "C", 2 },
                    { 40, 4, "C", 2 },
                    { 41, 5, "C", 2 },
                    { 42, 6, "C", 2 },
                    { 43, 1, "D", 2 },
                    { 44, 2, "D", 2 },
                    { 45, 3, "D", 2 },
                    { 46, 4, "D", 2 },
                    { 47, 5, "D", 2 },
                    { 48, 6, "D", 2 },
                    { 49, 1, "A", 3 },
                    { 50, 2, "A", 3 },
                    { 51, 3, "A", 3 },
                    { 52, 4, "A", 3 },
                    { 53, 5, "A", 3 },
                    { 54, 6, "A", 3 },
                    { 55, 1, "B", 3 },
                    { 56, 2, "B", 3 },
                    { 57, 3, "B", 3 },
                    { 58, 4, "B", 3 },
                    { 59, 5, "B", 3 },
                    { 60, 6, "B", 3 },
                    { 61, 1, "C", 3 },
                    { 62, 2, "C", 3 },
                    { 63, 3, "C", 3 },
                    { 64, 4, "C", 3 },
                    { 65, 5, "C", 3 },
                    { 66, 6, "C", 3 },
                    { 67, 1, "D", 3 },
                    { 68, 2, "D", 3 },
                    { 69, 3, "D", 3 },
                    { 70, 4, "D", 3 },
                    { 71, 5, "D", 3 },
                    { 72, 6, "D", 3 },
                    { 73, 1, "A", 4 },
                    { 74, 2, "A", 4 },
                    { 75, 3, "A", 4 },
                    { 76, 4, "A", 4 },
                    { 77, 5, "A", 4 },
                    { 78, 6, "A", 4 },
                    { 79, 1, "B", 4 },
                    { 80, 2, "B", 4 },
                    { 81, 3, "B", 4 },
                    { 82, 4, "B", 4 },
                    { 83, 5, "B", 4 },
                    { 84, 6, "B", 4 },
                    { 85, 1, "C", 4 },
                    { 86, 2, "C", 4 },
                    { 87, 3, "C", 4 },
                    { 88, 4, "C", 4 },
                    { 89, 5, "C", 4 },
                    { 90, 6, "C", 4 },
                    { 91, 1, "D", 4 },
                    { 92, 2, "D", 4 },
                    { 93, 3, "D", 4 },
                    { 94, 4, "D", 4 },
                    { 95, 5, "D", 4 },
                    { 96, 6, "D", 4 },
                    { 97, 1, "A", 5 },
                    { 98, 2, "A", 5 },
                    { 99, 3, "A", 5 },
                    { 100, 4, "A", 5 },
                    { 101, 5, "A", 5 },
                    { 102, 6, "A", 5 },
                    { 103, 1, "B", 5 },
                    { 104, 2, "B", 5 },
                    { 105, 3, "B", 5 },
                    { 106, 4, "B", 5 },
                    { 107, 5, "B", 5 },
                    { 108, 6, "B", 5 },
                    { 109, 1, "C", 5 },
                    { 110, 2, "C", 5 },
                    { 111, 3, "C", 5 },
                    { 112, 4, "C", 5 },
                    { 113, 5, "C", 5 },
                    { 114, 6, "C", 5 },
                    { 115, 1, "D", 5 },
                    { 116, 2, "D", 5 },
                    { 117, 3, "D", 5 },
                    { 118, 4, "D", 5 },
                    { 119, 5, "D", 5 },
                    { 120, 6, "D", 5 }
                });

            migrationBuilder.InsertData(
                table: "SeatAvailabilities",
                columns: new[] { "ScreeningId", "SeatId", "IsAvailable" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 1, 2, false },
                    { 1, 3, true },
                    { 1, 4, true },
                    { 1, 5, true },
                    { 1, 6, true },
                    { 1, 7, true },
                    { 1, 8, true },
                    { 1, 9, true },
                    { 1, 10, true },
                    { 1, 11, true },
                    { 1, 12, true },
                    { 1, 13, true },
                    { 1, 14, true },
                    { 1, 15, true },
                    { 1, 16, true },
                    { 1, 17, true },
                    { 1, 18, true },
                    { 1, 19, true },
                    { 1, 20, true },
                    { 1, 21, true },
                    { 1, 22, true },
                    { 1, 23, true },
                    { 1, 24, true },
                    { 2, 25, false },
                    { 2, 26, true },
                    { 2, 27, true },
                    { 2, 28, true },
                    { 2, 29, true },
                    { 2, 30, true },
                    { 2, 31, true },
                    { 2, 32, true },
                    { 2, 33, true },
                    { 2, 34, true },
                    { 2, 35, true },
                    { 2, 36, true },
                    { 2, 37, true },
                    { 2, 38, true },
                    { 2, 39, true },
                    { 2, 40, true },
                    { 2, 41, true },
                    { 2, 42, true },
                    { 2, 43, true },
                    { 2, 44, true },
                    { 2, 45, true },
                    { 2, 46, true },
                    { 2, 47, true },
                    { 2, 48, true },
                    { 3, 49, true },
                    { 3, 50, true },
                    { 3, 51, true },
                    { 3, 52, true },
                    { 3, 53, true },
                    { 3, 54, true },
                    { 3, 55, true },
                    { 3, 56, true },
                    { 3, 57, true },
                    { 3, 58, true },
                    { 3, 59, true },
                    { 3, 60, true },
                    { 3, 61, true },
                    { 3, 62, true },
                    { 3, 63, true },
                    { 3, 64, true },
                    { 3, 65, true },
                    { 3, 66, true },
                    { 3, 67, true },
                    { 3, 68, true },
                    { 3, 69, true },
                    { 3, 70, true },
                    { 3, 71, true },
                    { 3, 72, true },
                    { 4, 1, false },
                    { 4, 2, false },
                    { 4, 3, true },
                    { 4, 4, true },
                    { 4, 5, true },
                    { 4, 6, true },
                    { 4, 7, true },
                    { 4, 8, true },
                    { 4, 9, true },
                    { 4, 10, true },
                    { 4, 11, true },
                    { 4, 12, true },
                    { 4, 13, true },
                    { 4, 14, true },
                    { 4, 15, true },
                    { 4, 16, true },
                    { 4, 17, true },
                    { 4, 18, true },
                    { 4, 19, true },
                    { 4, 20, true },
                    { 4, 21, true },
                    { 4, 22, true },
                    { 4, 23, true },
                    { 4, 24, true },
                    { 5, 25, false },
                    { 5, 26, true },
                    { 5, 27, true },
                    { 5, 28, true },
                    { 5, 29, true },
                    { 5, 30, true },
                    { 5, 31, true },
                    { 5, 32, true },
                    { 5, 33, true },
                    { 5, 34, true },
                    { 5, 35, true },
                    { 5, 36, true },
                    { 5, 37, true },
                    { 5, 38, true },
                    { 5, 39, true },
                    { 5, 40, true },
                    { 5, 41, true },
                    { 5, 42, true },
                    { 5, 43, true },
                    { 5, 44, true },
                    { 5, 45, true },
                    { 5, 46, true },
                    { 5, 47, true },
                    { 5, 48, true },
                    { 6, 49, true },
                    { 6, 50, true },
                    { 6, 51, true },
                    { 6, 52, true },
                    { 6, 53, true },
                    { 6, 54, true },
                    { 6, 55, true },
                    { 6, 56, true },
                    { 6, 57, true },
                    { 6, 58, true },
                    { 6, 59, true },
                    { 6, 60, true },
                    { 6, 61, true },
                    { 6, 62, true },
                    { 6, 63, true },
                    { 6, 64, true },
                    { 6, 65, true },
                    { 6, 66, true },
                    { 6, 67, true },
                    { 6, 68, true },
                    { 6, 69, true },
                    { 6, 70, true },
                    { 6, 71, true },
                    { 6, 72, true },
                    { 7, 1, false },
                    { 7, 2, false },
                    { 7, 3, true },
                    { 7, 4, true },
                    { 7, 5, true },
                    { 7, 6, true },
                    { 7, 7, true },
                    { 7, 8, true },
                    { 7, 9, true },
                    { 7, 10, true },
                    { 7, 11, true },
                    { 7, 12, true },
                    { 7, 13, true },
                    { 7, 14, true },
                    { 7, 15, true },
                    { 7, 16, true },
                    { 7, 17, true },
                    { 7, 18, true },
                    { 7, 19, true },
                    { 7, 20, true },
                    { 7, 21, true },
                    { 7, 22, true },
                    { 7, 23, true },
                    { 7, 24, true },
                    { 8, 25, false },
                    { 8, 26, true },
                    { 8, 27, true },
                    { 8, 28, true },
                    { 8, 29, true },
                    { 8, 30, true },
                    { 8, 31, true },
                    { 8, 32, true },
                    { 8, 33, true },
                    { 8, 34, true },
                    { 8, 35, true },
                    { 8, 36, true },
                    { 8, 37, true },
                    { 8, 38, true },
                    { 8, 39, true },
                    { 8, 40, true },
                    { 8, 41, true },
                    { 8, 42, true },
                    { 8, 43, true },
                    { 8, 44, true },
                    { 8, 45, true },
                    { 8, 46, true },
                    { 8, 47, true },
                    { 8, 48, true },
                    { 9, 49, true },
                    { 9, 50, true },
                    { 9, 51, true },
                    { 9, 52, true },
                    { 9, 53, true },
                    { 9, 54, true },
                    { 9, 55, true },
                    { 9, 56, true },
                    { 9, 57, true },
                    { 9, 58, true },
                    { 9, 59, true },
                    { 9, 60, true },
                    { 9, 61, true },
                    { 9, 62, true },
                    { 9, 63, true },
                    { 9, 64, true },
                    { 9, 65, true },
                    { 9, 66, true },
                    { 9, 67, true },
                    { 9, 68, true },
                    { 9, 69, true },
                    { 9, 70, true },
                    { 9, 71, true },
                    { 9, 72, true },
                    { 10, 1, false },
                    { 10, 2, false },
                    { 10, 3, true },
                    { 10, 4, true },
                    { 10, 5, true },
                    { 10, 6, true },
                    { 10, 7, true },
                    { 10, 8, true },
                    { 10, 9, true },
                    { 10, 10, true },
                    { 10, 11, true },
                    { 10, 12, true },
                    { 10, 13, true },
                    { 10, 14, true },
                    { 10, 15, true },
                    { 10, 16, true },
                    { 10, 17, true },
                    { 10, 18, true },
                    { 10, 19, true },
                    { 10, 20, true },
                    { 10, 21, true },
                    { 10, 22, true },
                    { 10, 23, true },
                    { 10, 24, true },
                    { 11, 25, false },
                    { 11, 26, true },
                    { 11, 27, true },
                    { 11, 28, true },
                    { 11, 29, true },
                    { 11, 30, true },
                    { 11, 31, true },
                    { 11, 32, true },
                    { 11, 33, true },
                    { 11, 34, true },
                    { 11, 35, true },
                    { 11, 36, true },
                    { 11, 37, true },
                    { 11, 38, true },
                    { 11, 39, true },
                    { 11, 40, true },
                    { 11, 41, true },
                    { 11, 42, true },
                    { 11, 43, true },
                    { 11, 44, true },
                    { 11, 45, true },
                    { 11, 46, true },
                    { 11, 47, true },
                    { 11, 48, true },
                    { 12, 49, true },
                    { 12, 50, true },
                    { 12, 51, true },
                    { 12, 52, true },
                    { 12, 53, true },
                    { 12, 54, true },
                    { 12, 55, true },
                    { 12, 56, true },
                    { 12, 57, true },
                    { 12, 58, true },
                    { 12, 59, true },
                    { 12, 60, true },
                    { 12, 61, true },
                    { 12, 62, true },
                    { 12, 63, true },
                    { 12, 64, true },
                    { 12, 65, true },
                    { 12, 66, true },
                    { 12, 67, true },
                    { 12, 68, true },
                    { 12, 69, true },
                    { 12, 70, true },
                    { 12, 71, true },
                    { 12, 72, true },
                    { 13, 1, false },
                    { 13, 2, false },
                    { 13, 3, true },
                    { 13, 4, true },
                    { 13, 5, true },
                    { 13, 6, true },
                    { 13, 7, true },
                    { 13, 8, true },
                    { 13, 9, true },
                    { 13, 10, true },
                    { 13, 11, true },
                    { 13, 12, true },
                    { 13, 13, true },
                    { 13, 14, true },
                    { 13, 15, true },
                    { 13, 16, true },
                    { 13, 17, true },
                    { 13, 18, true },
                    { 13, 19, true },
                    { 13, 20, true },
                    { 13, 21, true },
                    { 13, 22, true },
                    { 13, 23, true },
                    { 13, 24, true },
                    { 14, 25, false },
                    { 14, 26, true },
                    { 14, 27, true },
                    { 14, 28, true },
                    { 14, 29, true },
                    { 14, 30, true },
                    { 14, 31, true },
                    { 14, 32, true },
                    { 14, 33, true },
                    { 14, 34, true },
                    { 14, 35, true },
                    { 14, 36, true },
                    { 14, 37, true },
                    { 14, 38, true },
                    { 14, 39, true },
                    { 14, 40, true },
                    { 14, 41, true },
                    { 14, 42, true },
                    { 14, 43, true },
                    { 14, 44, true },
                    { 14, 45, true },
                    { 14, 46, true },
                    { 14, 47, true },
                    { 14, 48, true },
                    { 15, 49, true },
                    { 15, 50, true },
                    { 15, 51, true },
                    { 15, 52, true },
                    { 15, 53, true },
                    { 15, 54, true },
                    { 15, 55, true },
                    { 15, 56, true },
                    { 15, 57, true },
                    { 15, 58, true },
                    { 15, 59, true },
                    { 15, 60, true },
                    { 15, 61, true },
                    { 15, 62, true },
                    { 15, 63, true },
                    { 15, 64, true },
                    { 15, 65, true },
                    { 15, 66, true },
                    { 15, 67, true },
                    { 15, 68, true },
                    { 15, 69, true },
                    { 15, 70, true },
                    { 15, 71, true },
                    { 15, 72, true },
                    { 16, 1, false },
                    { 16, 2, false },
                    { 16, 3, true },
                    { 16, 4, true },
                    { 16, 5, true },
                    { 16, 6, true },
                    { 16, 7, true },
                    { 16, 8, true },
                    { 16, 9, true },
                    { 16, 10, true },
                    { 16, 11, true },
                    { 16, 12, true },
                    { 16, 13, true },
                    { 16, 14, true },
                    { 16, 15, true },
                    { 16, 16, true },
                    { 16, 17, true },
                    { 16, 18, true },
                    { 16, 19, true },
                    { 16, 20, true },
                    { 16, 21, true },
                    { 16, 22, true },
                    { 16, 23, true },
                    { 16, 24, true },
                    { 17, 25, false },
                    { 17, 26, true },
                    { 17, 27, true },
                    { 17, 28, true },
                    { 17, 29, true },
                    { 17, 30, true },
                    { 17, 31, true },
                    { 17, 32, true },
                    { 17, 33, true },
                    { 17, 34, true },
                    { 17, 35, true },
                    { 17, 36, true },
                    { 17, 37, true },
                    { 17, 38, true },
                    { 17, 39, true },
                    { 17, 40, true },
                    { 17, 41, true },
                    { 17, 42, true },
                    { 17, 43, true },
                    { 17, 44, true },
                    { 17, 45, true },
                    { 17, 46, true },
                    { 17, 47, true },
                    { 17, 48, true },
                    { 18, 49, true },
                    { 18, 50, true },
                    { 18, 51, true },
                    { 18, 52, true },
                    { 18, 53, true },
                    { 18, 54, true },
                    { 18, 55, true },
                    { 18, 56, true },
                    { 18, 57, true },
                    { 18, 58, true },
                    { 18, 59, true },
                    { 18, 60, true },
                    { 18, 61, true },
                    { 18, 62, true },
                    { 18, 63, true },
                    { 18, 64, true },
                    { 18, 65, true },
                    { 18, 66, true },
                    { 18, 67, true },
                    { 18, 68, true },
                    { 18, 69, true },
                    { 18, 70, true },
                    { 18, 71, true },
                    { 18, 72, true },
                    { 19, 1, false },
                    { 19, 2, false },
                    { 19, 3, true },
                    { 19, 4, true },
                    { 19, 5, true },
                    { 19, 6, true },
                    { 19, 7, true },
                    { 19, 8, true },
                    { 19, 9, true },
                    { 19, 10, true },
                    { 19, 11, true },
                    { 19, 12, true },
                    { 19, 13, true },
                    { 19, 14, true },
                    { 19, 15, true },
                    { 19, 16, true },
                    { 19, 17, true },
                    { 19, 18, true },
                    { 19, 19, true },
                    { 19, 20, true },
                    { 19, 21, true },
                    { 19, 22, true },
                    { 19, 23, true },
                    { 19, 24, true },
                    { 20, 25, false },
                    { 20, 26, true },
                    { 20, 27, true },
                    { 20, 28, true },
                    { 20, 29, true },
                    { 20, 30, true },
                    { 20, 31, true },
                    { 20, 32, true },
                    { 20, 33, true },
                    { 20, 34, true },
                    { 20, 35, true },
                    { 20, 36, true },
                    { 20, 37, true },
                    { 20, 38, true },
                    { 20, 39, true },
                    { 20, 40, true },
                    { 20, 41, true },
                    { 20, 42, true },
                    { 20, 43, true },
                    { 20, 44, true },
                    { 20, 45, true },
                    { 20, 46, true },
                    { 20, 47, true },
                    { 20, 48, true },
                    { 21, 49, true },
                    { 21, 50, true },
                    { 21, 51, true },
                    { 21, 52, true },
                    { 21, 53, true },
                    { 21, 54, true },
                    { 21, 55, true },
                    { 21, 56, true },
                    { 21, 57, true },
                    { 21, 58, true },
                    { 21, 59, true },
                    { 21, 60, true },
                    { 21, 61, true },
                    { 21, 62, true },
                    { 21, 63, true },
                    { 21, 64, true },
                    { 21, 65, true },
                    { 21, 66, true },
                    { 21, 67, true },
                    { 21, 68, true },
                    { 21, 69, true },
                    { 21, 70, true },
                    { 21, 71, true },
                    { 21, 72, true }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "ScreeningId", "SeatId", "UserId" },
                values: new object[,]
                {
                    { 1, 12.99m, 1, 1, 2 },
                    { 2, 12.99m, 1, 2, 2 },
                    { 3, 14.99m, 2, 25, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_ScreenId",
                table: "Screenings",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Screens_CinemaId",
                table: "Screens",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAvailabilities_SeatId",
                table: "SeatAvailabilities",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScreenId",
                table: "Seats",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "SeatAvailabilities");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
