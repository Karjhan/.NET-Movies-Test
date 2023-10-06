using Core.Entities;

namespace Core.Specifications;

public class MoviesWithActorsAndGenresSpecification : BaseSpecification<Movie>
{
    public MoviesWithActorsAndGenresSpecification(MovieSpecificationParams movieSpecificationParams) : base()
    {
        AddInclude(movie => movie.Actors);
        AddInclude(movie => movie.Genres);
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
}