using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public record GetUserFollowedQuery(string UserId, int? Limit) : IQuery<List<FollowerDto>>;


