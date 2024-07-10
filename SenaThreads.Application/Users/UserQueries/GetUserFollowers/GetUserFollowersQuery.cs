using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowers;
public record GetUserFollowersQuery(string UserId, int? Limit) : IQuery<List<FollowerDto>>;
