namespace SenaThreads.Domain.Abstractions;

// Encapsula un conjunto de operaciones que afectan a la Base de datos, y las ejecuta en un sola transacción.
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
