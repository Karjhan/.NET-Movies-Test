namespace Core.Entities;

public class Actor : BaseEntity
{
    public string Name { get; set; }

    public List<Movie> Movies { get; set; }
}