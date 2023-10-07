using Core.Entities;

namespace Core.Specifications;

public class MoviesWithActorsAndGenresSpecification : BaseSpecification<Movie>
{
    public MoviesWithActorsAndGenresSpecification(MovieSpecificationParams movieSpecificationParams) 
        : base(movie =>
            (string.IsNullOrEmpty(movieSpecificationParams.Search) || movie.Name.ToLower().Contains(movieSpecificationParams.Search)) 
            &&
            (movieSpecificationParams.ActorIds == null || movieSpecificationParams.ActorIds.Count == 0 || movie.Actors.Select(actor => actor.Id).Contains(movieSpecificationParams.ActorIds[0])) 
            &&
            (!movieSpecificationParams.GenreId.HasValue || movie.Genres.Select(genre => genre.Id).Any(id => id == movieSpecificationParams.GenreId))
            )
    {
        AddInclude(movie => movie.Actors);
        AddInclude(movie => movie.Genres);
        ApplyPaging(movieSpecificationParams.PageSize * (movieSpecificationParams.PageIndex - 1), movieSpecificationParams.PageSize);
        if (!string.IsNullOrEmpty(movieSpecificationParams.Sort))
        {
            switch (movieSpecificationParams.Sort)
            {
                case "ratingAsc":
                    AddOrderBy(movie => movie.Rating);
                    break;
                case "ratingDesc":
                    AddOrderByDescending(movie => movie.Rating);
                    break;
                case "yearAsc":
                    AddOrderBy(movie => movie.Year);
                    break;
                case "yearDesc":
                    AddOrderByDescending(movie => movie.Year);
                    break;
                default:
                    AddOrderBy(movie => movie.Name);
                    break;
            }
        }
    }

    public MoviesWithActorsAndGenresSpecification(Guid id) : base(movie => movie.Id == id)
    {
        AddInclude(movie => movie.Actors);
        AddInclude(movie => movie.Genres);
    }
    
    //movie.Actors.Select(actor => actor.Id).ContainsMultiple(movieSpecificationParams.ActorIds) 
}