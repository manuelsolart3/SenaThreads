using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public record GetUserFollowedQuery(string userId, int? limit) : IQuery<FollowerResultDto>;


