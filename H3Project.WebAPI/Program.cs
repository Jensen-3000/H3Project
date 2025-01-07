using H3Project.Data.Context;
using H3Project.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

app.UseCors("AllowLocalhost"); // Use the CORS policy

app.Run();
