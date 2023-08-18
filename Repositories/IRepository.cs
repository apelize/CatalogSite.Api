using Entities;

namespace Repositories;

public interface IRepository<T> where T : Product
{
    Task CreateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
}