using Microsoft.EntityFrameworkCore;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext appDbContext)
    {
        _dbSet = appDbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }
}
