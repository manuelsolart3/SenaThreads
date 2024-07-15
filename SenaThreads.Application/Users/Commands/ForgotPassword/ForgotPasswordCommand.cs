using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.Commands.ForgotPassword;
public record ForgotPasswordCommand(string email) : ICommand;
