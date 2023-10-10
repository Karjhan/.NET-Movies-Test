using FakeItEasy;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace APITests.Services;

public class MovieContextSeedTests
{
    private async Task<MoviesContext> GetEmptyDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<MoviesContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new MoviesContext(options);
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
    
    private async Task<MoviesContext> GetFilledDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<MoviesContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new MoviesContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Movies.CountAsync() <= 0)
        {
            var newActor = new Actor() { Name = "RandomActorName", Movies = new List<Movie>() };
            var newGenre = new Genre() { Name = "RandomGenreName", Movies = new List<Movie>() };
            var newMovie = new Movie()
            {
                Name = "RandomMovieName", Description = "RandomMovieDescription", Year = 2023, Rating = 10,
                Actors = new List<Actor>() { newActor }, Genres = new List<Genre>() { newGenre },
                CoverURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Dr._goh_%2B_child.jpg/1024px-Dr._goh_%2B_child.jpg",
                ImdbURL = "RandomImdbURL"
            };
            newActor.Movies.Add(newMovie);
            newGenre.Movies.Add(newMovie);
            databaseContext.Movies.Add(newMovie);
            databaseContext.Actors.Add(newActor);
            databaseContext.Genres.Add(newGenre);
            if (databaseContext.ChangeTracker.HasChanges())
            {
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }


    [Test]
    public async Task MovieContextSeed_SeedAsync_FillsEmptyDB()
    {
        //Arrange
        var dbContext = await GetEmptyDatabaseContext();
        var hostingEnv = A.Fake<IHostingEnvironment>();
        hostingEnv.EnvironmentName = "Development";
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..") + "\\API\\wwwroot";
        hostingEnv.WebRootPath = wwwrootPath;

        //Act
        await MoviesContextSeed.SeedAsync(dbContext, hostingEnv);
        var count = await dbContext.Movies.CountAsync();
        
        //Assert
        Assert.That(count, Is.EqualTo(23));
    }
    
    [Test]
    public async Task MovieContextSeed_SeedAsync_DoesNotFillNonEmptyDB()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var hostingEnv = A.Fake<IHostingEnvironment>();
        hostingEnv.EnvironmentName = "Development";
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..") + "\\API\\wwwroot";
        hostingEnv.WebRootPath = wwwrootPath;

        //Act
        await MoviesContextSeed.SeedAsync(dbContext, hostingEnv);
        var count = await dbContext.Movies.CountAsync();
        
        //Assert
        Assert.That(count, Is.EqualTo(1));
    }
    
    [Test]
    public async Task MovieContextSeed_SeedAsync_DoesNotFillNonEmptyStaticImageRoot()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var hostingEnv = A.Fake<IHostingEnvironment>();
        hostingEnv.EnvironmentName = "Development";
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..") + "\\API\\wwwroot";
        hostingEnv.WebRootPath = wwwrootPath;

        //Act
        await MoviesContextSeed.SeedAsync(dbContext, hostingEnv);
        var count = Directory.EnumerateFileSystemEntries(wwwrootPath + "\\images\\movies").Count();
        
        //Assert
        Assert.That(count, Is.EqualTo(23));
    }
    
    [Test]
    public async Task MovieContextSeed_SeedAsync_FillsEmptyStaticImageRoot()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var hostingEnv = A.Fake<IHostingEnvironment>();
        hostingEnv.EnvironmentName = "Development";
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..") + "\\API\\wwwroot";
        hostingEnv.WebRootPath = wwwrootPath;
        var files = Directory.EnumerateFileSystemEntries(wwwrootPath + "\\images\\movies");
        foreach (var file in files)
        {
            File.Delete(file);
        }
        var emptyCount = Directory.EnumerateFileSystemEntries(wwwrootPath + "\\images\\movies").Count();
        Assert.That(emptyCount, Is.EqualTo(0));

        //Act
        await MoviesContextSeed.SeedAsync(dbContext, hostingEnv);
        var count = Directory.EnumerateFileSystemEntries(wwwrootPath + "\\images\\movies").Count();
        
        //Assert
        Assert.That(count, Is.EqualTo(23));
    }
}