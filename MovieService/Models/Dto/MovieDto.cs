namespace MovieService.Models.Dto
{
    public class MovieDto
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public long budget { get; set; }
        public List<GenreDto> genres { get; set; }
        public string homepage { get; set; }
        public long id { get; set; }
        public string imdb_id { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompanyDto> production_companies { get; set; }
        public List<ProductionCountryDto> production_countries { get; set; }
        public DateTime release_date { get; set; }
        public long revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguageDto> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
