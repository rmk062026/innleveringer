using MovieLibrary.Api.Model;

namespace MovieLibrary.Api.Services;

public class MovieService
{
    private readonly List<Movie> _movies = new List<Movie>();
    private int _nextId = 1;

    public Task<List<Movie>> GetAllAsync()
    {
        return Task.FromResult(_movies.ToList());
    }

    public Task<Movie?> GetByIdAsync(int id)
    {
        Movie? movie = _movies
            .FirstOrDefault(m => m.Id == id);

        return Task.FromResult(movie);
    }

    public Task<Movie> AddAsync(Movie movie)
    {
        movie.Id = _nextId++;
        movie.CreatedAt = DateTime.UtcNow;

        _movies.Add(movie);

        return Task.FromResult(movie);
    }
}