using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.SearchUsersByUsername;
public record SearchUsersByUsernameQuery(
    string searchTerm,
    string userId,
    int page,
    int pageSize) : IQuery<Pageable<UserDto>>;

