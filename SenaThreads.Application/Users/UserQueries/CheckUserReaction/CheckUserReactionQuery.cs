using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.CheckUserReaction;
public record CheckUserReactionQuery(string userId, Guid tweetId):IQuery<UserReactionCkeckDto>;
