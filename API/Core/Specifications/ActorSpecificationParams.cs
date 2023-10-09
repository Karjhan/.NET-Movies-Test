namespace Core.Specifications;

public class ActorSpecificationParams : PaginationParams
{
    private string _searchPattern;
    
    public string? Search
    {
        get => _searchPattern; 
        set => _searchPattern = value is null ? "" : value.ToLower();
    }
}