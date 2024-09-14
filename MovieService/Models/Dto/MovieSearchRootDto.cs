namespace MovieService.Models.Dto
{
    public class MovieSearchRootDto
    {
        public int page { get; set; }
        public List<MovieSearchResultDto>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
