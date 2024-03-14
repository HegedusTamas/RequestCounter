using Microsoft.EntityFrameworkCore;
using RequestCounter.DataAccess.DataContext;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RequestCounter.DataAccess.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly RequestContext _requestContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(RequestContext context)
    {
        _requestContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
        {
            throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}