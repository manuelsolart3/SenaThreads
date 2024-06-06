using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.IRepositories;
public interface IUserRepository 
{
    Task<User> GetUserByIdAsync(string userId);
}
