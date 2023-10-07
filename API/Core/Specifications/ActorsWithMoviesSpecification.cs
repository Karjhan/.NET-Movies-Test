using Core.Entities;

namespace Core.Specifications;

public class ActorsWithMoviesSpecification : BaseSpecification<Actor>
{
    public ActorsWithMoviesSpecification(ActorSpecificationParams actorSpecificationParams) 
        : base( actor => 
            string.IsNullOrEmpty(actorSpecificationParams.Search) || actor.Name.ToLower().Contains(actorSpecificationParams.Search)
            )
    {
        AddInclude(actor => actor.Movies);
        ApplyPaging(actorSpecificationParams.PageSize * (actorSpecificationParams.PageIndex - 1), actorSpecificationParams.PageSize);
    }
}