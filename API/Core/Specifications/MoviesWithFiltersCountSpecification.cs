using Core.Entities;

namespace Core.Specifications;

public class MoviesWithFiltersCountSpecification : BaseSpecification<Movie>
{
    public MoviesWithFiltersCountSpecification(MovieSpecificationParams movieSpecificationParams)
        : base(movie =>
            (string.IsNullOrEmpty(movieSpecificationParams.Search) ||
             movie.Name.ToLower().Contains(movieSpecificationParams.Search))
            &&
            (movieSpecificationParams.ActorIds == null || movieSpecificationParams.ActorIds.Count == 0 ||
             movie.Actors.Select(actor => actor.Id).Contains(movieSpecificationParams.ActorIds[0]))
            &&
            (!movieSpecificationParams.GenreId.HasValue || movie.Genres.Select(genre => genre.Id)
                .Any(id => id == movieSpecificationParams.GenreId))
        )
    {
        
    }
}