using Microsoft.EntityFrameworkCore;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly DbSet<TEntity> _dbSet;//referencia a la coleccion de entidades 
    protected readonly AppDbContext _context;

    public Repository(AppDbContext appDbContext)  
    {
        _context = appDbContext;
        _dbSet = appDbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
       
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
       return await _dbSet.FindAsync(id);
    }

    public IQueryable<TEntity> Queryable()
    {
        return _dbSet;
    }
}
