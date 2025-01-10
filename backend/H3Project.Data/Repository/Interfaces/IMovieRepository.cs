using H3Project.Data.Models;

namespace H3Project.Data.Repository.Interfaces;

public interface IMovieRepository : IGenericRepository<MovieModel>
{
    Task<MovieModel?> GetMovieWithDetailsAsync(int id);
    Task<MovieModel?> GetMovieBySlugAsync(string slug);
    Task UpdateMovieGenresAsync(MovieModel movie, List<int> genreIds);
}
