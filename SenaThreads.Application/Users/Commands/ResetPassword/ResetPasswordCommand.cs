using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.Commands.ResetPassword;
public record ResetPasswordCommand(string email, string token, string newPassword) : ICommand;
