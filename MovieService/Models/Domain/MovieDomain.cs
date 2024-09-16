using MovieService.Models.Dto;
using Newtonsoft.Json.Linq;

public class MovieDomain
{
    public string Title { get; private set; }
    public string OriginalTitle { get; private set; }
    public double AverageRating { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public string Overview { get; private set; }
    public List<string> SimilarMovies { get; private set; }

    private MovieDomain(string title, string originalTitle, double averageRating, DateTime? releaseDate, string overview, List<string> similarMovies)
    {
        Title = title;
        OriginalTitle = originalTitle;
        AverageRating = averageRating;
        ReleaseDate = releaseDate;
        Overview = overview;
        SimilarMovies = similarMovies;
    }
    public static MovieDomain Create(string title, string originalTitle, double averageRating, DateTime? releaseDate, string overview, List<MovieSearchResultWS> similarResults)
    {
        List<string> similarMovies = similarResults
                .Take(5)
                .Select(m => $"{m.title}{GetYearFromReleaseDate(m.release_date)}")
                .ToList();

        return new MovieDomain(title, originalTitle, averageRating, releaseDate, overview, similarMovies);
    }

    private static string GetYearFromReleaseDate(DateTime? release_date)
    {
        if (release_date != null)
        {
            DateTime releaseDateNotNull = release_date.Value;
            return $" ({releaseDateNotNull.Year})";
        }
        else return "";
    }
}
