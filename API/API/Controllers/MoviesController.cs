using API.DTO;
using API.Errors;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Pagination<MovieDTO>>> GetMovies([FromQuery] MovieSpecificationParams movieParams)
    {
        ISpecification<Movie> mainSpecification = new MoviesWithActorsAndGenresSpecification(movieParams);
        ISpecification<Movie> countSpecification = new MoviesWithFiltersCountSpecification(movieParams);
        int totalItemsCount = await _movieRepository.CountAsync(countSpecification);
        IReadOnlyList<Movie> movies = await _movieRepository.GetAllWithSpecificationAsync(mainSpecification);
        IReadOnlyList<MovieDTO> result = _mapper.Map<IReadOnlyList<Movie>, IReadOnlyList<MovieDTO>>(movies);
        return Ok(new Pagination<MovieDTO>(movieParams.PageIndex, movieParams.PageSize, totalItemsCount, result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Movie>> GetMovieById(Guid id)
    {
        ISpecification<Movie> specification = new MoviesWithActorsAndGenresSpecification(id);
        Movie movie = await _movieRepository.GetEntityWithSpecificationAsync(specification);
        if (movie is null)
        {
            return NotFound(new APIResponse(404));
        }
        MovieDTO result = _mapper.Map<Movie, MovieDTO>(movie);
        return Ok(result);
    }
    
    [HttpGet("actors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Pagination<ActorDTO>>> GetActors([FromQuery] ActorSpecificationParams actorParams)
    {
        ISpecification<Actor> specification = new ActorsWithMoviesSpecification(actorParams);
        int totalItemsCount = await _actorRepository.CountAsync(specification);
        IReadOnlyList<Actor> actors = await _actorRepository.GetAllWithSpecificationAsync(specification);
        IReadOnlyList<ActorDTO> result = _mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorDTO>>(actors);
        return Ok(new Pagination<ActorDTO>(actorParams.PageIndex, actorParams.PageSize, totalItemsCount, result));
    }
        
    [HttpGet("genres")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<GenreDTO>>> GetGenres([FromQuery] GenreSpecificationParams genreParams)
    {
        ISpecification<Genre> specification = new GenresStockSpecification(genreParams);
        int totalItemsCount = await _genreRepository.CountAsync(specification);
        IReadOnlyList<Genre> genres = await _genreRepository.GetAllWithSpecificationAsync(specification);
        IReadOnlyList<GenreDTO> result = _mapper.Map<IReadOnlyList<Genre>, IReadOnlyList<GenreDTO>>(genres);
        return Ok(new Pagination<GenreDTO>(genreParams.PageIndex, genreParams.PageSize, totalItemsCount, result));
    }
}