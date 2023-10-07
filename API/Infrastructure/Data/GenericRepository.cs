using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private MoviesContext _moviesContext;

    public GenericRepository(MoviesContext moviesContext)
    {
        _moviesContext = moviesContext;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _moviesContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _moviesContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpecification(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_moviesContext.Set<T>().AsQueryable(), specification);
    }
}