using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; }
    
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    
    public Expression<Func<T, object>> OrderBy { get; private set; }
    
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public BaseSpecification()
    {
        
    }
    
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> expressionToInclude)
    {
        Includes.Add(expressionToInclude);
    }

    protected void AddOrderBy(Expression<Func<T, object>> newOrderByExpression)
    {
        OrderBy = newOrderByExpression;
    }
    
    protected void AddOrderByDescending(Expression<Func<T, object>> newOrderByDescendingExpression)
    {
        OrderByDescending = newOrderByDescendingExpression;
    }
}