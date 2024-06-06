namespace SenaThreads.Domain.Abstractions;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid id); //son operaciones Async porque requiren acceso a la Bd
    IQueryable<TEntity> Queryable();
}
