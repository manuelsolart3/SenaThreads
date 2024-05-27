namespace SenaThreads.Domain.Abstractions;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    void Add(TEntity entity);
}
