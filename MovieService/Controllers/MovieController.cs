using Microsoft.AspNetCore.Mvc;
using MovieService.Models.Response;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly MovieServices _movieService;

    public MovieController(MovieServices movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("{title}")]
    public async Task<IActionResult> GetMovie(string title)
    {
        MovieResponse movie = await _movieService.GetMovieByTitleAsync(title);
        if (!movie.Success) return BadRequest();
        return Ok(movie.MovieDomain);
    }
}
