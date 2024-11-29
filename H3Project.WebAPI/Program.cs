using H3Project.Data.Context;
using H3Project.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Services / models
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<CinemaRepository>();
builder.Services.AddScoped<CinemaDetailRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<ScreenRepository>();
builder.Services.AddScoped<SeatRepository>();
builder.Services.AddScoped<ShowtimeRepository>();
builder.Services.AddScoped<TicketRepository>();


var conn = builder.Configuration.GetConnectionString("LocalDbCinema");
builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlServer(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
