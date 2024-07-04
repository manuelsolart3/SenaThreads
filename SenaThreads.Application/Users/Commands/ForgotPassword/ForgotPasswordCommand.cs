using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.Commands.ForgotPassword;
public record ForgotPasswordCommand(string Email) : IRequest<Result>;
