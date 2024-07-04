using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.Commands.ResetPassword;
public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Result>;
