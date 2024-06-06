using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowers;
public record GetUserFollowersQuery(string UserId) : IQuery<List<FollowerDto>>;
