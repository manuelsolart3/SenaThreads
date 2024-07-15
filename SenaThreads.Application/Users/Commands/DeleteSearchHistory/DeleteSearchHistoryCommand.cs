using MediatR;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.DeleteSearchHistory;
public record DeleteSearchHistoryCommand(string userId) : ICommand<Unit>;
