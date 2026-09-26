
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Model;
using MovieLibrary.Api.Services;

namespace MovieLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAll(
        [FromQuery] string? genre,
        [FromQuery] int? year,
        [FromQuery] string sortBy = "title",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest("Page must be greater than 0.");
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(
                "PageSize must be between 1 and 100.");
        }

        var movies = await _movieService.GetAllAsync(
            genre,
            year,
            sortBy,
            page,
            pageSize);

        return Ok(movies);
    }




    // GET: api/movies/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie is null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    // POST: api/movies
    [HttpPost]
    public async Task<ActionResult<Movie>> Create(Movie movie)
    {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            return BadRequest("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(movie.Director))
        {
            return BadRequest("Director is required.");
        }

        if (movie.ReleaseYear < 1888 ||
            movie.ReleaseYear > DateTime.UtcNow.Year + 5)
        {
            return BadRequest("Invalid release year.");
        }

        var createdMovie = await _movieService.AddAsync(movie);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdMovie.Id },
            createdMovie
        );
    }
}
