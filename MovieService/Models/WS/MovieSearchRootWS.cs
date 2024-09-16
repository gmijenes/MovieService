namespace MovieService.Models.Dto
{
    public class MovieSearchRootWS
    {
        public int page { get; set; }
        public List<MovieSearchResultWS>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
