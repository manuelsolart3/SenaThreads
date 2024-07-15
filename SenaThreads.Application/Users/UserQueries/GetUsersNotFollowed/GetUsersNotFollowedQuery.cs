using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.GetUsersNotFollowed;
public record GetUsersNotFollowedQuery(string userId, int page, int pageSize) : IQuery<Pageable<FollowerDto>>;

