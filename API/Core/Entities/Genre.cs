namespace Core.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }
    
    public List<Movie> Movies { get; set; }
}