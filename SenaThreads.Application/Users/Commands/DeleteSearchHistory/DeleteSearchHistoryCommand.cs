using MediatR;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.DeleteSearchHistory;
public record DeleteSearchHistoryCommand(string UserId) : ICommand<Unit>;
