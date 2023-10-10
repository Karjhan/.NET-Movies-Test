namespace Core.Entities;

public class Movie : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public int Year { get; set; }

    public decimal Rating { get; set; }

    public List<Genre> Genres { get; set; }

    public List<Actor> Actors { get; set; }

    public string? CoverURL { get; set; }

    public string? ImdbURL { get; set; }
}