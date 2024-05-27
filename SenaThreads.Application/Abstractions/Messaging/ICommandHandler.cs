using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Abstractions.Messaging;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
