using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System;
using MovieService.Models.Dto;
using MovieService.Models.Response;

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

    public async Task<MovieResponse> GetMovieByTitleAsync(string title)
    {
        string cacheKey = $"movie_{title}";
        if (_cache.TryGetValue(cacheKey, out MovieDomain cachedMovie))
        {
            return MovieResponse.Create(cachedMovie, true); ;
        }

        (string, bool) idResp = await GetMovieIdByTitleAsync(title);
      
        if (idResp.Item2 ) { 

            MovieWS? details = await GetMovieDetailsByIdAsync(idResp.Item1);
            MovieSearchRootWS? similarRoot = await GetMovieSimilarByIdAsync(idResp.Item1);

            if(details != null && similarRoot != null)
            {
                List<MovieSearchResultWS> similarResults = similarRoot.results ?? new List<MovieSearchResultWS>();
                var movieResult = MovieDomain.Create(details.title, details.original_title, details.vote_average, details.release_date, details.overview, similarResults);
                _cache.Set(cacheKey, movieResult, TimeSpan.FromMinutes(30));
                return MovieResponse.Create(movieResult, true);
            }
            return MovieResponse.Create(null, true);

        }

        return MovieResponse.Create(null, false);

    }

    private async Task<(string, bool)> GetMovieIdByTitleAsync(string title)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={title}");
            var json = JObject.Parse(response);
            MovieSearchRootWS? movieList = json.ToObject<MovieSearchRootWS>();
            if ( movieList.results.Count > 0)
            {
                var movie = movieList.results.First();
                var movieId = movie.id.ToString();
                return (movieId, true);
            }
            return (string.Empty, true);
            
        } 
        catch (Exception ex)
        {
            return (string.Empty, false);
        }
       
    }

    private async Task<MovieWS?> GetMovieDetailsByIdAsync(string movieId)
    {
        try
        {
            var detailsResponse = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}");
            var detailsJson = JObject.Parse(detailsResponse);
            return detailsJson.ToObject<MovieWS>();
        } 
        catch (Exception ex) {
            return null;
        }
    }

    private async Task<MovieSearchRootWS?> GetMovieSimilarByIdAsync(string movieId)
    {
        try
        {
            var similarResponse = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{movieId}/similar?api_key={_apiKey}");
            var similarJson = JObject.Parse(similarResponse);
            return similarJson.ToObject<MovieSearchRootWS>();
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}
