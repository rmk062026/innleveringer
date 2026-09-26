
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Model;
using MovieLibrary.Api.Services;
using MovieLibrary.Api.DTOs;

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
            ModelState.AddModelError(
                nameof(page),
                "Page must be greater than 0.");
        }

        if (pageSize < 1 || pageSize > 100)
        {
            ModelState.AddModelError(
                nameof(pageSize),
                "PageSize must be between 1 and 100.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
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
    public async Task<ActionResult<Movie>> Create(
        [FromBody] CreateMovieRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            ModelState.AddModelError(
                nameof(request.Title),
                "Title cannot be empty or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(request.Director))
        {
            ModelState.AddModelError(
                nameof(request.Director),
                "Director cannot be empty or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(request.Genre))
        {
            ModelState.AddModelError(
                nameof(request.Genre),
                "Genre cannot be empty or whitespace.");
        }

        if (request.ReleaseYear > DateTime.UtcNow.Year + 5)
        {
            ModelState.AddModelError(
                nameof(request.ReleaseYear),
                "Release year is too far in the future.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var movie = new Movie
        {
            Title = request.Title.Trim(),
            Director = request.Director.Trim(),
            Genre = request.Genre.Trim(),
            ReleaseYear = request.ReleaseYear
        };

        var createdMovie = await _movieService.AddAsync(movie);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdMovie.Id },
            createdMovie);
    }

}
