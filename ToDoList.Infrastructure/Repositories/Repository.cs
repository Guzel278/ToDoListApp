using Microsoft.EntityFrameworkCore;
using ToDoList.DataBase;

namespace ToDoList.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public T Add(T entity)
    {
        _dbSet.Add(entity); 
        _context.SaveChanges(); 
        return entity;
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public IQueryable<T> GetAllQuery() => _dbSet.AsNoTracking();

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return;
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

