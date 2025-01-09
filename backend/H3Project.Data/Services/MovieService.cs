using H3Project.Data.DTOs.Movies;
using H3Project.Data.DTOs.Schedules;
using H3Project.Data.Models;
using H3Project.Data.Repository.Interfaces;
using H3Project.Data.Services.Interfaces;

namespace H3Project.Data.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;

    public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
    }

    public async Task<List<MovieReadDto>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        return movies.Select(MapModelToReadDto).ToList();
    }

    public async Task<MovieReadDto?> GetMovieByIdAsync(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);
        return movie == null ? null : MapModelToReadDto(movie);
    }

    public async Task<MovieReadDto?> GetMovieBySlugAsync(string slug)
    {
        var movie = await _movieRepository.GetMovieBySlugAsync(slug);
        return movie == null ? null : MapModelToReadDto(movie);
    }

    public async Task<MovieReadDto> CreateMovieAsync(MovieCreateDto movieCreateDto)
    {
        var genres = await _genreRepository.GetAllGenresAsync();
        var selectedGenres = genres.Where(g => movieCreateDto.GenreIds.Contains(g.Id)).ToList();

        var movie = MapCreateDtoToModel(movieCreateDto, selectedGenres);
        await _movieRepository.AddMovieAsync(movie);

        return MapModelToReadDto(movie);
    }

    public async Task<bool> UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto)
    {
        if (id != movieUpdateDto.Id)
        {
            return false;
        }

        var movie = await _movieRepository.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return false;
        }

        var genres = await _genreRepository.GetAllGenresAsync();
        var selectedGenres = genres.Where(g => movieUpdateDto.GenreIds.Contains(g.Id)).ToList();

        MapUpdateDtoToModel(movieUpdateDto, movie, selectedGenres);
        await _movieRepository.UpdateMovieAsync(movie);

        return true;
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return false;
        }

        await _movieRepository.DeleteMovieAsync(movie);
        return true;
    }

    private static MovieReadDto MapModelToReadDto(Movie movie)
    {
        return new MovieReadDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Slug = movie.Slug,
            Description = movie.Description,
            ImageUrl = movie.ImageUrl,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            Genres = movie.Genres.Select(g => g.Name).ToList(),
            CurrentSchedules = movie.Schedules
                .Select(s => new ScheduleReadDto
                {
                    Id = s.Id,
                    ShowTime = s.ShowTime,
                    EndTime = s.EndTime,
                    BasePrice = s.BasePrice,
                    TheaterId = s.TheaterId,
                    TheaterName = s.Theater.Name,
                    MovieId = s.MovieId,
                    MovieTitle = s.Movie.Title
                }).ToList()
        };
    }

    private static Movie MapCreateDtoToModel(MovieCreateDto movieCreateDto, List<Genre> genres) => new()
    {
        Title = movieCreateDto.Title,
        Description = movieCreateDto.Description,
        ReleaseDate = movieCreateDto.ReleaseDate,
        Duration = movieCreateDto.Duration,
        Genres = genres
    };

    private static void MapUpdateDtoToModel(MovieUpdateDto movieUpdateDto, Movie movie, List<Genre> genres)
    {
        movie.Title = movieUpdateDto.Title;
        movie.Description = movieUpdateDto.Description;
        movie.ReleaseDate = movieUpdateDto.ReleaseDate;
        movie.Duration = movieUpdateDto.Duration;
        movie.Genres = genres;
    }
}