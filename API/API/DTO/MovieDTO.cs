using Core.Entities;

namespace API.DTO;

public class MovieDTO
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public int Year { get; set; }

    public decimal Rating { get; set; }

    public List<string> Genres { get; set; }

    public List<string> Actors { get; set; }

    public string CoverURL { get; set; }

    public string ImdbURL { get; set; }
}