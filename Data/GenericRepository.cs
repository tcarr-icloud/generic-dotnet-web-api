using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace webapi;

public class GenericRepository<TEntity> where TEntity : class
{
    private readonly DatabaseApiContext _databaseApiContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DatabaseApiContext databaseApiContext)
    {
        _databaseApiContext = databaseApiContext;
        _dbSet = databaseApiContext.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null)
            return orderBy(query).ToList();
        return query.ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (entity.GetType() == typeof(User))
        {
            User user = (User)Convert.ChangeType(entity, typeof(User));
            if (Guid.Empty == user.Guid) user.Guid = Guid.NewGuid();
            entity = (TEntity)Convert.ChangeType(user, typeof(TEntity));
        }
        
        _dbSet.Add(entity);
        await _databaseApiContext.SaveChangesAsync();
        return entity;
    }
    public virtual void Delete(object id)
    {
        var entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_databaseApiContext.Entry(entityToDelete).State == EntityState.Detached) _dbSet.Attach(entityToDelete);

        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _databaseApiContext.Entry(entityToUpdate).State = EntityState.Modified;
    }
}