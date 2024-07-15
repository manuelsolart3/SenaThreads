using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.Commands.ValidateToken;
public record ValidateTokenCommand(string email, string token) : ICommand<TokenValidationResultDto>;


