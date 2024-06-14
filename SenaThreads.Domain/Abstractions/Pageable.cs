namespace SenaThreads.Domain.Abstractions;

public class Pageable<TEntity>
    where TEntity : class
{
    public List<TEntity> List { get; set; }
    public int Count { get; set; }
}


