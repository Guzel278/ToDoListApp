
namespace ToDoList.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    public T Add(T entity);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    IQueryable<T> GetAllQuery();
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}

