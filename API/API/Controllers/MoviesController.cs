using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseAPIController
{
    private readonly IGenericRepository<Movie> _movieRepository;
    
    private readonly IGenericRepository<Actor> _actorRepository;
    
    private readonly IGenericRepository<Genre> _genreRepository;

    private readonly IMapper _mapper;

    public MoviesController(IGenericRepository<Movie> movieRepository, IGenericRepository<Actor> actorRepository, IGenericRepository<Genre> genreRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies([FromQuery] MovieSpecificationParams movieParams)
    {
        ISpecification<Movie> specification = new MoviesWithActorsAndGenresSpecification(movieParams);
        IReadOnlyList<Movie> movies = await _movieRepository.GetAllWithSpecificationAsync(specification);
        IReadOnlyList<MovieDTO> result = _mapper.Map<IReadOnlyList<Movie>, IReadOnlyList<MovieDTO>>(movies);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(Guid id)
    {
        ISpecification<Movie> specification = new MoviesWithActorsAndGenresSpecification(id);
        Movie movie = await _movieRepository.GetEntityWithSpecification(specification);
        if (movie is null)
        {
            return NotFound();
        }
        MovieDTO result = _mapper.Map<Movie, MovieDTO>(movie);
        return Ok(result);
    }
}