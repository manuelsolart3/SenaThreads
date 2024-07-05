using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.CheckUserBlockStatus;
public record CheckUserBlockStatusQuery(
    string BlockedUserId,
    string BlockByUserId) : IQuery<UserBlockStatusDto>;
