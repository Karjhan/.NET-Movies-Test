using Core.Entities;

namespace Core.Specifications;

public class GenresStockSpecification : BaseSpecification<Genre>
{
    public GenresStockSpecification(GenreSpecificationParams genreSpecificationParams) 
        : base( genre => 
            string.IsNullOrEmpty(genreSpecificationParams.Search) || genre.Name.ToLower().Contains(genreSpecificationParams.Search)
        )
    {
        ApplyPaging(genreSpecificationParams.PageSize * (genreSpecificationParams.PageIndex - 1), genreSpecificationParams.PageSize);
    }
}