using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure;
using Infrastructure.Data.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace APITests.Repositories;

public class MovieRepositoryTests
{
    private async Task<MoviesContext> GetFilledDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<MoviesContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new MoviesContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Movies.CountAsync() <= 0)
        {
            var newActor1 = new Actor() { Name = "RandomActor1Name", Movies = new List<Movie>(), Id = new Guid("acd4d0e9-6a44-4874-a6e4-58d5ed82934e")};
            var newActor2 = new Actor() { Name = "RandomActor2Name", Movies = new List<Movie>(), Id = new Guid("98409c19-6a8c-4ad6-b983-22e2b5d89f87") };
            var newActor3 = new Actor() { Name = "RandomActor3Name", Movies = new List<Movie>(), Id = new Guid("633bd454-71f8-44f5-86d4-64f7783dcfab") };
            var newGenre1 = new Genre() { Name = "RandomGenre1Name", Movies = new List<Movie>(), Id = new Guid("0e1fb368-f239-4678-a783-a6be5f82995a") };
            var newGenre2 = new Genre() { Name = "RandomGenre2Name", Movies = new List<Movie>(), Id = new Guid("23e0c38a-2b6e-42a3-a110-c4a901f52639") };
            var newMovie1 = new Movie()
            {
                Name = "RandomMovie1Name", Description = "RandomMovie1Description", Year = 2023, Rating = 10,
                Actors = new List<Actor>() { newActor1, newActor2 }, Genres = new List<Genre>() { newGenre1 },
                CoverURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Dr._goh_%2B_child.jpg/1024px-Dr._goh_%2B_child.jpg",
                ImdbURL = "RandomImdb1URL", Id = new Guid("a7d340ca-3f64-4bff-b115-20c3466be238")
            };
            var newMovie2 = new Movie()
            {
                Name = "RandomMovie2Name", Description = "RandomMovie2Description", Year = 2022, Rating = 5,
                Actors = new List<Actor>() { newActor2, newActor3 }, Genres = new List<Genre>() { newGenre1, newGenre2 },
                CoverURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Dr._goh_%2B_child.jpg/1024px-Dr._goh_%2B_child.jpg",
                ImdbURL = "RandomImdb2URL", Id = new Guid("8b5858cf-612d-45ea-94ab-7f4a71c407c3")
            };
            newActor1.Movies.Add(newMovie1);
            newActor2.Movies.Add(newMovie1);
            newActor2.Movies.Add(newMovie2);
            newActor3.Movies.Add(newMovie1);
            newGenre1.Movies.Add(newMovie1);
            newGenre1.Movies.Add(newMovie2);
            newGenre2.Movies.Add(newMovie2);
            var actors = new List<Actor>() { newActor1, newActor2, newActor3 };
            var genres = new List<Genre>() { newGenre1, newGenre2 };
            var movies = new List<Movie>() { newMovie1, newMovie2 };
            databaseContext.Movies.AddRange(movies);
            databaseContext.Actors.AddRange(actors);
            databaseContext.Genres.AddRange(genres);
            if (databaseContext.ChangeTracker.HasChanges())
            {
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }

    [Test]
    public async Task MovieRepository_GetAllAsync_ReturnsCorrectAmount()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var movieRepo = new GenericRepository<Movie>(dbContext);

        //Act
        var data = await movieRepo.GetAllAsync();

        //Assert
        Assert.AreEqual(2, data.Count);
    }
    
    [Test]
    public async Task MovieRepository_GetByIdAsync_ReturnsCorrectObject()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var movieRepo = new GenericRepository<Movie>(dbContext);

        //Act
        var data = await movieRepo.GetByIdAsync(new Guid("8b5858cf-612d-45ea-94ab-7f4a71c407c3"));

        //Assert
        Assert.AreEqual(2, data.Genres.Count);
    }
    
    [Test]
    public async Task MovieRepository_GetEntityWithSpecificationAsync_ReturnsCorrectObject()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var movieRepo = new GenericRepository<Movie>(dbContext);
        var id = new Guid("a7d340ca-3f64-4bff-b115-20c3466be238");
        var specification = new MoviesWithActorsAndGenresSpecification(id);

        //Act
        var data = await movieRepo.GetEntityWithSpecificationAsync(specification);

        //Assert
        Assert.AreEqual(1, data.Genres.Count);
    }
    
    [Test]
    public async Task MovieRepository_GetAllWithSpecificationAsync_ReturnsCorrectObjects()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var movieRepo = new GenericRepository<Movie>(dbContext);
        var genreId = new Guid("0e1fb368-f239-4678-a783-a6be5f82995a");
        var movieParams = new MovieSpecificationParams() { GenreId = genreId };
        var specification = new MoviesWithActorsAndGenresSpecification(movieParams);

        //Act
        var data = await movieRepo.GetAllWithSpecificationAsync(specification);

        //Assert
        Assert.AreEqual(2, data.Count);
    }
    
    [Test]
    public async Task MovieRepository_CountAsync_ReturnsCorrectAmount()
    {
        //Arrange
        var dbContext = await GetFilledDatabaseContext();
        var movieRepo = new GenericRepository<Movie>(dbContext);
        var movieParams = new MovieSpecificationParams() { Search = "RandomMovie1" };
        var specification = new MoviesWithFiltersCountSpecification(movieParams);

        //Act
        var count = await movieRepo.CountAsync(specification);

        //Assert
        Assert.AreEqual(1, count);
    }
}