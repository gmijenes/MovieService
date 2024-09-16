namespace MovieService.Models.Response
{
    public class MovieResponse
    {
        public MovieDomain? MovieDomain { get; private set; }
        public bool Success { get; private set; }

        private MovieResponse(MovieDomain? movie, bool success)
        {
            this.MovieDomain = movie;
            this.Success = success;
        }

        public static MovieResponse Create(MovieDomain? movie, bool success)
        {
            return new MovieResponse(movie, success);
        }
    }
}
