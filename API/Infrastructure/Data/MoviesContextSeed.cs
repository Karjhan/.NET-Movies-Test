using System.Reflection;
using System.Text.RegularExpressions;
using Core.Entities;
using Infrastructure.Data.DataContexts;
using Microsoft.AspNetCore.Hosting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Data;

public class MoviesContextSeed
{
    public static async Task SeedAsync(MoviesContext context, IHostingEnvironment _environment)
    {
        var mainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var staticsPath = _environment.IsDevelopment() ? _environment.WebRootPath + "\\images\\movies" : _environment.WebRootPath + "/images/movies";
        if (!Directory.EnumerateFileSystemEntries(staticsPath).Any())
        {
            string jsonString = File.ReadAllText(mainPath + "/Data/SeedData/movies.json");
            IncomingData moviesData = JsonSerializer.Deserialize<IncomingData>(jsonString);
            using (var httpClient = new HttpClient())
            {
                foreach (var data in moviesData.movies)
                {
                    var uri = new Uri(data.cover);
                    var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                    var fileExtension = Path.GetExtension(uriWithoutQuery);
                    var fileName = Regex.Replace(data.name.Replace(" ", "_"), @"[\\/:*?^<>|]", "_");
                    var path = Path.Combine(_environment.WebRootPath + "\\images\\movies", $"{fileName}{fileExtension}");
                    var imageBytes = await httpClient.GetByteArrayAsync(uri);
                    await File.WriteAllBytesAsync(path, imageBytes);
                }
            }
        }
        if (!context.Movies.Any() || !context.Genres.Any() || !context.Actors.Any())
        {
            string jsonString = File.ReadAllText(mainPath + "/Data/SeedData/movies.json");
            IncomingData moviesData = JsonSerializer.Deserialize<IncomingData>(jsonString);
            List<Movie> movies = new List<Movie>();
            List<Actor> actors = new List<Actor>();
            List<Genre> genres = new List<Genre>();
            foreach (var data in moviesData.movies)
            {
                foreach (var genreName in data.genre)
                {
                    if (genres.All(genre => genre.Name != genreName))
                    {
                        genres.Add(new Genre(){Name = genreName, Movies = new List<Movie>()});
                    }
                }
                foreach (var actorName in data.stars)
                {
                    if (actors.All(actor => actor.Name != actorName))
                    {
                        actors.Add(new Actor(){Name = actorName, Movies = new List<Movie>()});
                    }
                }
            }
            foreach (var data in moviesData.movies)
            {
                var uri = new Uri(data.cover);
                var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                var fileExtension = Path.GetExtension(uriWithoutQuery);
                var fileName = Regex.Replace(data.name.Replace(" ", "_"), @"[\\/:*?^<>|]", "_");
                Movie newMovie = new Movie()
                {
                    Name = data.name,
                    Description = data.description,
                    Year = data.year,
                    Rating = data.rating,
                    CoverURL = $"/images/movies/{fileName}{fileExtension}",
                    ImdbURL = data.imdb,
                    Genres = new List<Genre>(),
                    Actors = new List<Actor>()
                };
                movies.Add(newMovie);
                foreach (var genreName in data.genre)
                {
                    Genre foundGenre = genres.First(genre => genre.Name == genreName);
                    foundGenre.Movies.Add(newMovie);
                    newMovie.Genres.Add(foundGenre);
                }
                foreach (var actorName in data.stars)
                {
                    Actor foundActor = actors.First(actor => actor.Name == actorName);
                    foundActor.Movies.Add(newMovie);
                    newMovie.Actors.Add(foundActor);
                }
            }
            context.Movies.AddRange(movies);
            context.Actors.AddRange(actors);
            context.Genres.AddRange(genres);
        }

        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}