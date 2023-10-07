using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    public Task<T?> GetByIdAsync(Guid id);

    public Task<IReadOnlyList<T>> GetAllAsync();

    public Task<T?> GetEntityWithSpecification(ISpecification<T> specification);

    public Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);
    
    public Task<int> CountAsync(ISpecification<T> specification); 
}