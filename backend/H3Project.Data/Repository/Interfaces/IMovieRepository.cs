using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie?> GetMovieBySlugAsync(string slug);
    Task AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(Movie movie);
}