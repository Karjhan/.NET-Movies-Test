namespace Core.Specifications;

public class MovieSpecificationParams : PaginationParams
{
    public string? Sort { get; set; }

    public List<Guid>? ActorIds { get; set; }

    public Guid? GenreId { get; set; }
}