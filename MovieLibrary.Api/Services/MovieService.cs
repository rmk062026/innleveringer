using MovieLibrary.Api.Model;

namespace MovieLibrary.Api.Services;

public class MovieService
{
    private readonly List<Movie> _movies = new List<Movie>();
    private int _nextId = 1;




    public Task<List<Movie>> GetAllAsync(
        string? genre = null,
        int? year = null,
        string sortBy = "title",
        int page = 1,
        int pageSize = 10)
    {
        IEnumerable<Movie> movies = _movies;

        // Filtrering etter sjanger
        if (!string.IsNullOrWhiteSpace(genre))
        {
            movies = movies.Where(m =>
                m.Genre.Equals(
                    genre,
                    StringComparison.OrdinalIgnoreCase));
        }

        // Filtrering etter år
        if (year.HasValue)
        {
            movies = movies.Where(m =>
                m.ReleaseYear == year.Value);
        }

        // Sortering
        movies = sortBy.ToLowerInvariant() switch
        {
            "title_desc" => movies.OrderByDescending(m => m.Title),
            "year" => movies.OrderBy(m => m.ReleaseYear),
            "year_desc" => movies.OrderByDescending(m => m.ReleaseYear),
            _ => movies.OrderBy(m => m.Title)
        };

        // Paginering
        movies = movies
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return Task.FromResult(movies.ToList());
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