namespace Core.Entities;

public class MovieData
{
    public string name { get; set; }
    
    public string description { get; set; }
    
    public int year { get; set; }
    
    public decimal rating { get; set; }
    
    public List<string> genre { get; set; }
    
    public List<string> stars { get; set; }
    
    public string cover { get; set; }
    
    public string imdb { get; set; }
}