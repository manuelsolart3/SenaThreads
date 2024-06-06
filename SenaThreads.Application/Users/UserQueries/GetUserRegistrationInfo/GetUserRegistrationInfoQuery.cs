using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserRegistrationInfo;
public record GetUserRegistrationInfoQuery(string UserId) : IQuery<UserRegistrationInfoDto>;
