using System.Linq.Expressions;

namespace RequestCounter.DataAccess.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
}