namespace Core.Specifications;

public class MovieSpecificationParams : PaginationParams
{
    private string _searchPattern;
    
    public string? Sort { get; set; }

    public List<Guid>? ActorIds { get; set; }

    public Guid? GenreId { get; set; }
    
    public string? Search
    {
        get => _searchPattern; 
        set => _searchPattern = value is null ? "" : value.ToLower();
    }
}