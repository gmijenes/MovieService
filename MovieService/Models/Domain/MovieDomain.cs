using MovieService.Models.Dto;
using Newtonsoft.Json.Linq;

public class MovieDomain
{
    public string Title { get; private set; }
    public string OriginalTitle { get; private set; }
    public double AverageRating { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string Overview { get; private set; }
    public List<string> SimilarMovies { get; private set; }

    private MovieDomain(string title, string originalTitle, double averageRating, DateTime releaseDate, string overview, List<string> similarMovies)
    {
        Title = title;
        OriginalTitle = originalTitle;
        AverageRating = averageRating;
        ReleaseDate = releaseDate;
        Overview = overview;
        SimilarMovies = similarMovies;
    }
    public static MovieDomain create(string title, string originalTitle, double averageRating, DateTime releaseDate, string overview, SimilarRootDto similar)
    {
        List<string> similarMovies = similar.results
                .Take(5)
                .Select(m => $"{m.title} ({m.release_date.Year})")
                .ToList();

        return new MovieDomain(title, originalTitle, averageRating, releaseDate, overview, similarMovies);
    }


}
