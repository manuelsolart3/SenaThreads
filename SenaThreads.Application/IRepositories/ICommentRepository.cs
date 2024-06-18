using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.IRepositories;
public interface ICommentRepository : IRepository<Comment>
{
}
