namespace MovieService.Models.Dto
{
    public class SimilarRootDto
    {
        public int page { get; set; }
        public List<SimilarElemDto> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
