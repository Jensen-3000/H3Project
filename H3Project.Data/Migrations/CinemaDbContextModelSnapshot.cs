﻿// <auto-generated />
using System;
using H3Project.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace H3Project.Data.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    partial class CinemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "MoviesMovieId");

                    b.HasIndex("MoviesMovieId");

                    b.ToTable("GenreMovie");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            BookingDate = new DateTime(2024, 11, 29, 10, 47, 51, 807, DateTimeKind.Local).AddTicks(4962),
                            CustomerId = 1
                        },
                        new
                        {
                            BookingId = 2,
                            BookingDate = new DateTime(2024, 11, 30, 10, 47, 51, 807, DateTimeKind.Local).AddTicks(5563),
                            CustomerId = 2
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Cinema", b =>
                {
                    b.Property<int>("CinemaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CinemaId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CinemaId");

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            CinemaId = 1,
                            Name = "Cinema One"
                        },
                        new
                        {
                            CinemaId = 2,
                            Name = "Cinema Two"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.CinemaDetail", b =>
                {
                    b.Property<int>("CinemaDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CinemaDetailId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CinemaDetailId");

                    b.HasIndex("CinemaId")
                        .IsUnique();

                    b.ToTable("CinemaDetails");

                    b.HasData(
                        new
                        {
                            CinemaDetailId = 1,
                            Address = "123 Main St",
                            CinemaId = 1,
                            PhoneNumber = "123-456-7890"
                        },
                        new
                        {
                            CinemaDetailId = 2,
                            Address = "456 Elm St",
                            CinemaId = 2,
                            PhoneNumber = "987-654-3210"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Name = "John Doe"
                        },
                        new
                        {
                            CustomerId = 2,
                            Name = "Jane Doe"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CinemaId");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            CinemaId = 1,
                            Name = "Alice",
                            RoleId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            CinemaId = 2,
                            Name = "Bob",
                            RoleId = 2
                        },
                        new
                        {
                            EmployeeId = 3,
                            CinemaId = 1,
                            Name = "Charlie",
                            RoleId = 2
                        },
                        new
                        {
                            EmployeeId = 4,
                            CinemaId = 2,
                            Name = "David",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Action"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Comedy"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            Title = "Movie One"
                        },
                        new
                        {
                            MovieId = 2,
                            Title = "Movie Two"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Manager"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "Staff"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Screen", b =>
                {
                    b.Property<int>("ScreenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScreenId"));

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScreenId");

                    b.HasIndex("CinemaId");

                    b.ToTable("Screens");

                    b.HasData(
                        new
                        {
                            ScreenId = 1,
                            CinemaId = 1,
                            Name = "Screen One"
                        },
                        new
                        {
                            ScreenId = 2,
                            CinemaId = 2,
                            Name = "Screen Two"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatId"));

                    b.Property<int>("ScreenId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SeatId");

                    b.HasIndex("ScreenId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            SeatId = 1,
                            ScreenId = 1,
                            SeatNumber = "A1"
                        },
                        new
                        {
                            SeatId = 2,
                            ScreenId = 1,
                            SeatNumber = "A2"
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Showtime", b =>
                {
                    b.Property<int>("ShowtimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShowtimeId"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ScreenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ShowtimeId");

                    b.HasIndex("MovieId");

                    b.HasIndex("ScreenId");

                    b.ToTable("Showtimes");

                    b.HasData(
                        new
                        {
                            ShowtimeId = 1,
                            MovieId = 1,
                            ScreenId = 1,
                            StartTime = new DateTime(2024, 11, 29, 10, 47, 51, 804, DateTimeKind.Local).AddTicks(5691)
                        },
                        new
                        {
                            ShowtimeId = 2,
                            MovieId = 2,
                            ScreenId = 2,
                            StartTime = new DateTime(2024, 11, 29, 12, 47, 51, 807, DateTimeKind.Local).AddTicks(2375)
                        });
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<int>("ShowtimeId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("BookingId");

                    b.HasIndex("SeatId");

                    b.HasIndex("ShowtimeId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            BookingId = 1,
                            SeatId = 1,
                            ShowtimeId = 1
                        },
                        new
                        {
                            TicketId = 2,
                            BookingId = 2,
                            SeatId = 2,
                            ShowtimeId = 2
                        });
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("H3Project.Data.Models.Domain.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Booking", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.CinemaDetail", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Cinema", "Cinema")
                        .WithOne("CinemaDetail")
                        .HasForeignKey("H3Project.Data.Models.Domain.CinemaDetail", "CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Employee", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Cinema", "Cinema")
                        .WithMany("Employees")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("H3Project.Data.Models.Domain.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Screen", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Cinema", "Cinema")
                        .WithMany("Screens")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Seat", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Screen", "Screen")
                        .WithMany("Seats")
                        .HasForeignKey("ScreenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Screen");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Showtime", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("H3Project.Data.Models.Domain.Screen", "Screen")
                        .WithMany("Showtimes")
                        .HasForeignKey("ScreenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Screen");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Ticket", b =>
                {
                    b.HasOne("H3Project.Data.Models.Domain.Booking", "Booking")
                        .WithMany("Tickets")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("H3Project.Data.Models.Domain.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("H3Project.Data.Models.Domain.Showtime", "Showtime")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Seat");

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Booking", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Cinema", b =>
                {
                    b.Navigation("CinemaDetail")
                        .IsRequired();

                    b.Navigation("Employees");

                    b.Navigation("Screens");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Movie", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Screen", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("H3Project.Data.Models.Domain.Showtime", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
