using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserInfo;
public record GetUserInfoQuery: IQuery<UserInfoDto>;
