using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.SearchUsersByUsername;
public record SearchUsersByUsernameQuery(
    string SearchTerm,
    string UserId,
    int Page,
    int PageSize) : IQuery<Pageable<UserDto>>;

