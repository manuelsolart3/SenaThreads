using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Abstractions.Messaging;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
