using API.DTO;
using API.Helpers;
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
    public async Task<ActionResult<Pagination<MovieDTO>>> GetMovies([FromQuery] MovieSpecificationParams movieParams)
    {
        ISpecification<Movie> mainSpecification = new MoviesWithActorsAndGenresSpecification(movieParams);
        ISpecification<Movie> countSpecification = new MoviesWithFiltersCountSpecification(movieParams);
        int totalItems = await _movieRepository.CountAsync(countSpecification);
        IReadOnlyList<Movie> movies = await _movieRepository.GetAllWithSpecificationAsync(mainSpecification);
        IReadOnlyList<MovieDTO> result = _mapper.Map<IReadOnlyList<Movie>, IReadOnlyList<MovieDTO>>(movies);
        return Ok(new Pagination<MovieDTO>(movieParams.PageIndex, movieParams.PageSize, totalItems, result));
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
    
    [HttpGet("actors")]
    public async Task<ActionResult<IReadOnlyList<ActorDTO>>> GetActors()
    {
        ISpecification<Actor> specification = new ActorsWithMoviesSpecification();
        IReadOnlyList<Actor> actors = await _actorRepository.GetAllWithSpecificationAsync(specification);
        IReadOnlyList<ActorDTO> result = _mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorDTO>>(actors);
        return Ok(result);
    }
        
    [HttpGet("genres")]
    public async Task<ActionResult<IReadOnlyList<GenreDTO>>> GetGenres()
    {
        IReadOnlyList<Genre> genres = await _genreRepository.GetAllAsync();
        IReadOnlyList<GenreDTO> result = _mapper.Map<IReadOnlyList<Genre>, IReadOnlyList<GenreDTO>>(genres);
        return Ok(result);
    }
}