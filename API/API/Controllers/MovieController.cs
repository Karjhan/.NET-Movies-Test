using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MovieController : BaseAPIController
{
    private readonly IGenericRepository<Movie> _movieRepository;
    
    private readonly IGenericRepository<Actor> _actorRepository;
    
    private readonly IGenericRepository<Genre> _genreRepository;

    public MovieController(IGenericRepository<Movie> movieRepository, IGenericRepository<Actor> actorRepository, IGenericRepository<Genre> genreRepository)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
        _genreRepository = genreRepository;
    }

    [HttpGet("movies")]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        IReadOnlyList<Movie> products = await _movieRepository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(Guid id)
    {
        return await _movieRepository.GetByIdAsync(id);
    }
}