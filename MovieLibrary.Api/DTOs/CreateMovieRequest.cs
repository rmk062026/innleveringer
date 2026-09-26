
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Api.DTOs;

public class CreateMovieRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(1)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Director is required.")]
    [MinLength(1)]
    public string Director { get; set; } = string.Empty;

    [Range(1888, 2100,
        ErrorMessage = "Release year must be between 1888 and 2100.")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Genre is required.")]
    public string Genre { get; set; } = string.Empty;
}
