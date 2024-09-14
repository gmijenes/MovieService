using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System;
using MovieService.Models.Dto;

public class MovieServices
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly IMemoryCache _cache;

    public MovieServices(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _apiKey = configuration["TMD:ApiKey"];
        _cache = cache;
    }

    public async Task<MovieDomain?> GetMovieByTitleAsync(string title)
    {
        string cacheKey = $"movie_{title}";
        if (_cache.TryGetValue(cacheKey, out MovieDomain cachedMovie))
        {
            return cachedMovie;
        }

        string movieId = await GetMovieIdByTitleAsync(title);
      
        MovieDto? details = await GetMovieDetailsByIdAsync(movieId);
        MovieSearchRootDto? similarRoot = await GetMovieSimilarByIdAsync(movieId);

        if(details != null && similarRoot != null)
        {
            List<MovieSearchResultDto> similarResults = similarRoot.results ?? new List<MovieSearchResultDto>();
            var movieResult = MovieDomain.create(details.title, details.original_title, details.vote_average, details.release_date, details.overview, similarResults);
            _cache.Set(cacheKey, movieResult, TimeSpan.FromMinutes(30));
            return movieResult;
        }

        return null;

    }

    private async Task<string> GetMovieIdByTitleAsync(string title)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={title}");
            var json = JObject.Parse(response);
            MovieSearchRootDto? movieList = json.ToObject<MovieSearchRootDto>();
            if (movieList != null && movieList.results != null)
            {
                var movie = movieList.results.First();
                var movieId = movie.id.ToString();
                return movieId;
            }
            return string.Empty;
            
        } 
        catch (Exception ex)
        {
            return string.Empty;
        }
       
    }

    private async Task<MovieDto?> GetMovieDetailsByIdAsync(string movieId)
    {
        try
        {
            var detailsResponse = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}");
            var detailsJson = JObject.Parse(detailsResponse);
            return detailsJson.ToObject<MovieDto>();
        } 
        catch (Exception ex) {
            return null;
        }
    }

    private async Task<MovieSearchRootDto?> GetMovieSimilarByIdAsync(string movieId)
    {
        try
        {
            var similarResponse = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{movieId}/similar?api_key={_apiKey}");
            var similarJson = JObject.Parse(similarResponse);
            return similarJson.ToObject<MovieSearchRootDto>();
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}
