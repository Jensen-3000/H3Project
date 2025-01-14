using H3Project.Data.Context;
using H3Project.Data.Mappings;
using H3Project.Data.Models;
using H3Project.Data.Repository;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:4200") // Allow your Angular app
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// EF DbContext
builder.Services.AddScoped<AppDbContext>();


var connectionString = builder.Configuration.GetConnectionString("LocalDbCinema");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// JWT
builder.Services.Configure<JwtSettingsModel>(builder.Configuration.GetSection("JwtSettings"));

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    });

// Authorization policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", authBuilder => { authBuilder.RequireRole("Admin"); })
    .AddPolicy("User", authBuilder => { authBuilder.RequireRole("User"); });


// Services and Repositories
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IScreenRepository, ScreenRepository>();
builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
builder.Services.AddScoped<ISeatAvailabilityRepository, SeatAvailabilityRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IScreenService, ScreenService>();
builder.Services.AddScoped<IScreeningService, ScreeningService>();
builder.Services.AddScoped<ISeatAvailabilityService, SeatAvailabilityService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowLocalhost"); // Use the CORS policy

app.Run();
