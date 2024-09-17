# Movie Service
Movie Service API is a RESTful application built in .NET 6 that consumes The Movie Database (TMDB) API. The service provides detailed information about specific movies and a list of similar movies. Its goal is to facilitate retrieving movie-related data for integrations or movie display applications.
## Dependencies and Development
### Main Technologies:
* .NET 6 SDK
* Docker
* Postman for testing the service 
### Extra dependencies 
* Access to The Movie Database API (TMDB) [here](https://developers.themoviedb.org/3/getting-started/introduction).
### Development steps:
1. **Clone the repository** from GitHub root:

`` git clone https://github.com/your-username repo-movie-service-api.git 
``

2. **Install dependencies:** Make sure you have the .NET 6 SDK installed. If not, download it [here](https://dotnet.microsoft.com/es-es/download/dotnet/6.0). Then restore the NuGet packages in the project directory: 
   
``dotnet restore ``

3. **Configure TMDB API Key:** Ensure you obtain an API key from The Movie Database. Add it to the appsettings.json configuration file in the following format:

```json
{
  "TMDB": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

When running the application in development mode, you have access to Swagger UI for testing purposes.

### Running as container
1. **Installing dependencies**: Make sure you have Docker installed. If not, you can install it from the official website [here](https://www.docker.com/). 

2. **Build the docker image** from the _MovieService/dockerfile_:
   
``docker build -t movie-service:latest . ``

3. **Run the container**:

`` docker run -d -p 8080:80 movie-service ``

At this point, the service should be running in the container and ready to receive requests.

### Making requests to the container
1. **Install postman:** To send requests to the server, it is recommended to use Postman. You can install it from [here](https://www.postman.com/downloads/).

2. **Add the postman collection file** _Movie Service.postman_collection.json_ as a new collection in your postman app, select _Get movie by title_ request and send. 


If there were no errors in the process, you will receive a response in the following format:

```json
{
    "title": "The Call",
    "originalTitle": "The Call",
    "averageRating": 6.798,
    "releaseDate": "2013-03-14T00:00:00",
    "overview": "Jordan Turner is an experienced 911 operator but when she makes an error in judgment and a call ends badly, Jordan is rattled and unsure if she can continue. But when teenager Casey Welson is abducted in the back of a man's car and calls 911, Jordan is the one called upon to use all of her experience, insights and quick thinking to help Casey escape, and not just to save her, but to make sure the man is brought to justice.",
    "similarMovies": [
        "Gang Related (1997)",
        "The Heir Apparent: Largo Winch (2008)",
        "Body Heat (1981)",
        "My Bloody Valentine (2009)",
        "Kill Switch (2008)"
    ]
}
```



