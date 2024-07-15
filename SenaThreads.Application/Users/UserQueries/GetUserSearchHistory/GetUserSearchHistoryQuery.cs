using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserSearchHistory;
public record GetUserSearchHistoryQuery(string userId, int limit) : IQuery<List<UserSearchHistoryDto>>;
