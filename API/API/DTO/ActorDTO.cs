namespace API.DTO;

public class ActorDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public List<string> Movies { get; set; }
}