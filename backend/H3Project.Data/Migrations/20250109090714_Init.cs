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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
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
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theaters_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
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
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Seats_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "123 Movie St", "Grand Cinema" },
                    { 2, "456 Central Ave", "Downtown Cinema" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "ReleaseDate", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, "High-speed action", new TimeSpan(0, 2, 30, 0, 0), "https://placehold.co/400x600?text=Fast+And+Furious+10", new DateOnly(2024, 1, 15), "fast-and-furious-10", "Fast & Furious 10" },
                    { 2, "Hilarious comedy", new TimeSpan(0, 1, 45, 0, 0), "https://placehold.co/400x600?text=Laugh+Out+Loud", new DateOnly(2024, 2, 10), "laugh-out-loud", "Laugh Out Loud" },
                    { 3, "Heartfelt drama", new TimeSpan(0, 2, 10, 0, 0), "https://placehold.co/400x600?text=Deep+Emotions", new DateOnly(2024, 3, 5), "deep-emotions", "Deep Emotions" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "GenreMovie",
                columns: new[] { "GenresId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Theaters",
                columns: new[] { "Id", "CinemaId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Screen 1" },
                    { 2, 1, "Screen 2" },
                    { 3, 2, "Screen 1" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserRoleId", "Username" },
                values: new object[,]
                {
                    { 1, "john@example.com", "hashed_password_1", 1, "john_doe" },
                    { 2, "jane@example.com", "hashed_password_2", 2, "jane_smith" },
                    { 3, "alice@example.com", "hashed_password_3", 2, "alice_jones" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "BasePrice", "EndTime", "MovieId", "ShowTime", "TheaterId" },
                values: new object[,]
                {
                    { 1, 12.99m, new DateTime(2024, 1, 16, 20, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 14.99m, new DateTime(2024, 1, 16, 23, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 9.99m, new DateTime(2024, 2, 11, 16, 45, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 2, 11, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "IsAvailable", "Number", "Row", "ScheduleId", "TheaterId" },
                values: new object[,]
                {
                    { 1, true, 1, "A", null, 1 },
                    { 2, true, 2, "A", null, 1 },
                    { 3, true, 3, "A", null, 1 },
                    { 4, true, 4, "A", null, 1 },
                    { 5, true, 5, "A", null, 1 },
                    { 6, true, 6, "A", null, 1 },
                    { 7, true, 7, "A", null, 1 },
                    { 8, true, 8, "A", null, 1 },
                    { 9, true, 9, "A", null, 1 },
                    { 10, true, 10, "A", null, 1 },
                    { 11, true, 11, "A", null, 1 },
                    { 12, true, 12, "A", null, 1 },
                    { 13, true, 1, "B", null, 1 },
                    { 14, true, 2, "B", null, 1 },
                    { 15, true, 3, "B", null, 1 },
                    { 16, true, 4, "B", null, 1 },
                    { 17, true, 5, "B", null, 1 },
                    { 18, true, 6, "B", null, 1 },
                    { 19, true, 7, "B", null, 1 },
                    { 20, true, 8, "B", null, 1 },
                    { 21, true, 9, "B", null, 1 },
                    { 22, true, 10, "B", null, 1 },
                    { 23, true, 11, "B", null, 1 },
                    { 24, true, 12, "B", null, 1 },
                    { 25, true, 1, "C", null, 1 },
                    { 26, true, 2, "C", null, 1 },
                    { 27, true, 3, "C", null, 1 },
                    { 28, true, 4, "C", null, 1 },
                    { 29, true, 5, "C", null, 1 },
                    { 30, true, 6, "C", null, 1 },
                    { 31, true, 7, "C", null, 1 },
                    { 32, true, 8, "C", null, 1 },
                    { 33, true, 9, "C", null, 1 },
                    { 34, true, 10, "C", null, 1 },
                    { 35, true, 11, "C", null, 1 },
                    { 36, true, 12, "C", null, 1 },
                    { 37, true, 1, "D", null, 1 },
                    { 38, true, 2, "D", null, 1 },
                    { 39, true, 3, "D", null, 1 },
                    { 40, true, 4, "D", null, 1 },
                    { 41, true, 5, "D", null, 1 },
                    { 42, true, 6, "D", null, 1 },
                    { 43, true, 7, "D", null, 1 },
                    { 44, true, 8, "D", null, 1 },
                    { 45, true, 9, "D", null, 1 },
                    { 46, true, 10, "D", null, 1 },
                    { 47, true, 11, "D", null, 1 },
                    { 48, true, 12, "D", null, 1 },
                    { 49, true, 1, "E", null, 1 },
                    { 50, true, 2, "E", null, 1 },
                    { 51, true, 3, "E", null, 1 },
                    { 52, true, 4, "E", null, 1 },
                    { 53, true, 5, "E", null, 1 },
                    { 54, true, 6, "E", null, 1 },
                    { 55, true, 7, "E", null, 1 },
                    { 56, true, 8, "E", null, 1 },
                    { 57, true, 9, "E", null, 1 },
                    { 58, true, 10, "E", null, 1 },
                    { 59, true, 11, "E", null, 1 },
                    { 60, true, 12, "E", null, 1 },
                    { 61, true, 1, "F", null, 1 },
                    { 62, true, 2, "F", null, 1 },
                    { 63, true, 3, "F", null, 1 },
                    { 64, true, 4, "F", null, 1 },
                    { 65, true, 5, "F", null, 1 },
                    { 66, true, 6, "F", null, 1 },
                    { 67, true, 7, "F", null, 1 },
                    { 68, true, 8, "F", null, 1 },
                    { 69, true, 9, "F", null, 1 },
                    { 70, true, 10, "F", null, 1 },
                    { 71, true, 11, "F", null, 1 },
                    { 72, true, 12, "F", null, 1 },
                    { 73, true, 1, "G", null, 1 },
                    { 74, true, 2, "G", null, 1 },
                    { 75, true, 3, "G", null, 1 },
                    { 76, true, 4, "G", null, 1 },
                    { 77, true, 5, "G", null, 1 },
                    { 78, true, 6, "G", null, 1 },
                    { 79, true, 7, "G", null, 1 },
                    { 80, true, 8, "G", null, 1 },
                    { 81, true, 9, "G", null, 1 },
                    { 82, true, 10, "G", null, 1 },
                    { 83, true, 11, "G", null, 1 },
                    { 84, true, 12, "G", null, 1 },
                    { 85, true, 1, "H", null, 1 },
                    { 86, true, 2, "H", null, 1 },
                    { 87, true, 3, "H", null, 1 },
                    { 88, true, 4, "H", null, 1 },
                    { 89, true, 5, "H", null, 1 },
                    { 90, true, 6, "H", null, 1 },
                    { 91, true, 7, "H", null, 1 },
                    { 92, true, 8, "H", null, 1 },
                    { 93, true, 9, "H", null, 1 },
                    { 94, true, 10, "H", null, 1 },
                    { 95, true, 11, "H", null, 1 },
                    { 96, true, 12, "H", null, 1 },
                    { 97, true, 1, "A", null, 2 },
                    { 98, true, 2, "A", null, 2 },
                    { 99, true, 3, "A", null, 2 },
                    { 100, true, 4, "A", null, 2 },
                    { 101, true, 5, "A", null, 2 },
                    { 102, true, 6, "A", null, 2 },
                    { 103, true, 7, "A", null, 2 },
                    { 104, true, 8, "A", null, 2 },
                    { 105, true, 9, "A", null, 2 },
                    { 106, true, 10, "A", null, 2 },
                    { 107, true, 1, "B", null, 2 },
                    { 108, true, 2, "B", null, 2 },
                    { 109, true, 3, "B", null, 2 },
                    { 110, true, 4, "B", null, 2 },
                    { 111, true, 5, "B", null, 2 },
                    { 112, true, 6, "B", null, 2 },
                    { 113, true, 7, "B", null, 2 },
                    { 114, true, 8, "B", null, 2 },
                    { 115, true, 9, "B", null, 2 },
                    { 116, true, 10, "B", null, 2 },
                    { 117, true, 1, "C", null, 2 },
                    { 118, true, 2, "C", null, 2 },
                    { 119, true, 3, "C", null, 2 },
                    { 120, true, 4, "C", null, 2 },
                    { 121, true, 5, "C", null, 2 },
                    { 122, true, 6, "C", null, 2 },
                    { 123, true, 7, "C", null, 2 },
                    { 124, true, 8, "C", null, 2 },
                    { 125, true, 9, "C", null, 2 },
                    { 126, true, 10, "C", null, 2 },
                    { 127, true, 1, "D", null, 2 },
                    { 128, true, 2, "D", null, 2 },
                    { 129, true, 3, "D", null, 2 },
                    { 130, true, 4, "D", null, 2 },
                    { 131, true, 5, "D", null, 2 },
                    { 132, true, 6, "D", null, 2 },
                    { 133, true, 7, "D", null, 2 },
                    { 134, true, 8, "D", null, 2 },
                    { 135, true, 9, "D", null, 2 },
                    { 136, true, 10, "D", null, 2 },
                    { 137, true, 1, "E", null, 2 },
                    { 138, true, 2, "E", null, 2 },
                    { 139, true, 3, "E", null, 2 },
                    { 140, true, 4, "E", null, 2 },
                    { 141, true, 5, "E", null, 2 },
                    { 142, true, 6, "E", null, 2 },
                    { 143, true, 7, "E", null, 2 },
                    { 144, true, 8, "E", null, 2 },
                    { 145, true, 9, "E", null, 2 },
                    { 146, true, 10, "E", null, 2 },
                    { 147, true, 1, "F", null, 2 },
                    { 148, true, 2, "F", null, 2 },
                    { 149, true, 3, "F", null, 2 },
                    { 150, true, 4, "F", null, 2 },
                    { 151, true, 5, "F", null, 2 },
                    { 152, true, 6, "F", null, 2 },
                    { 153, true, 7, "F", null, 2 },
                    { 154, true, 8, "F", null, 2 },
                    { 155, true, 9, "F", null, 2 },
                    { 156, true, 10, "F", null, 2 },
                    { 157, true, 1, "A", null, 3 },
                    { 158, true, 2, "A", null, 3 },
                    { 159, true, 3, "A", null, 3 },
                    { 160, true, 4, "A", null, 3 },
                    { 161, true, 5, "A", null, 3 },
                    { 162, true, 6, "A", null, 3 },
                    { 163, true, 7, "A", null, 3 },
                    { 164, true, 8, "A", null, 3 },
                    { 165, true, 1, "B", null, 3 },
                    { 166, true, 2, "B", null, 3 },
                    { 167, true, 3, "B", null, 3 },
                    { 168, true, 4, "B", null, 3 },
                    { 169, true, 5, "B", null, 3 },
                    { 170, true, 6, "B", null, 3 },
                    { 171, true, 7, "B", null, 3 },
                    { 172, true, 8, "B", null, 3 },
                    { 173, true, 1, "C", null, 3 },
                    { 174, true, 2, "C", null, 3 },
                    { 175, true, 3, "C", null, 3 },
                    { 176, true, 4, "C", null, 3 },
                    { 177, true, 5, "C", null, 3 },
                    { 178, true, 6, "C", null, 3 },
                    { 179, true, 7, "C", null, 3 },
                    { 180, true, 8, "C", null, 3 },
                    { 181, true, 1, "D", null, 3 },
                    { 182, true, 2, "D", null, 3 },
                    { 183, true, 3, "D", null, 3 },
                    { 184, true, 4, "D", null, 3 },
                    { 185, true, 5, "D", null, 3 },
                    { 186, true, 6, "D", null, 3 },
                    { 187, true, 7, "D", null, 3 },
                    { 188, true, 8, "D", null, 3 },
                    { 189, true, 1, "E", null, 3 },
                    { 190, true, 2, "E", null, 3 },
                    { 191, true, 3, "E", null, 3 },
                    { 192, true, 4, "E", null, 3 },
                    { 193, true, 5, "E", null, 3 },
                    { 194, true, 6, "E", null, 3 },
                    { 195, true, 7, "E", null, 3 },
                    { 196, true, 8, "E", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "PurchaseDate", "ScheduleId", "SeatId", "UserId" },
                values: new object[,]
                {
                    { 1, 12.99m, new DateTime(2025, 1, 4, 10, 7, 14, 93, DateTimeKind.Local).AddTicks(8346), 1, 1, 2 },
                    { 2, 14.99m, new DateTime(2025, 1, 6, 10, 7, 14, 96, DateTimeKind.Local).AddTicks(6109), 2, 98, 3 },
                    { 3, 9.99m, new DateTime(2025, 1, 8, 10, 7, 14, 96, DateTimeKind.Local).AddTicks(6131), 3, 157, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MovieId",
                table: "Schedules",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TheaterId",
                table: "Schedules",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_Row_Number_TheaterId",
                table: "Seats",
                columns: new[] { "Row", "Number", "TheaterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScheduleId",
                table: "Seats",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TheaterId",
                table: "Seats",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_CinemaId",
                table: "Theaters",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScheduleId",
                table: "Tickets",
                column: "ScheduleId");

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
                name: "GenreMovie");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Theaters");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
