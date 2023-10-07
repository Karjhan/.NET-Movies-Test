using Core.Entities;

namespace Core.Specifications;

public class ActorsWithMoviesSpecification : BaseSpecification<Actor>
{
    public ActorsWithMoviesSpecification() : base()
    {
        AddInclude(actor => actor.Movies);
    }
    
    // public ActorsWithMoviesSpecification(Guid id) : base(actor => actor.Id == id)
    // {
    //     AddInclude(actor => actor.Movies);
    // }
}