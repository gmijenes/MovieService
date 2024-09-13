using Microsoft.AspNetCore.Mvc;
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
        var movie = await _movieService.GetMovieByTitleAsync(title);
        if (movie != null)
            return Ok(movie);
        else return BadRequest();
    }
}
