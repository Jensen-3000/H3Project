using H3Project.Data.Context;
using H3Project.Data.Mappings;
using H3Project.Data.Models;
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

// Services / models
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:4200") // Allow your Angular app
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// EF DbContext
builder.Services.AddScoped<IAppDbContext, AppDbContext>();


var conn = builder.Configuration.GetConnectionString("LocalDbCinema");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conn));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

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
    .AddPolicy("Customer", authBuilder => { authBuilder.RequireRole("Customer"); });

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
