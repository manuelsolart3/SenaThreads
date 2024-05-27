using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Abstractions.Messaging;

// Al definir 2 interfaces (ICommand y ICommand<TResponse>),
// estamos dando la flexibilidad de que todos nuestros Commands retornen un resultado (Result),
// y además estamos dando la polibilidad de que retornen algún tipo de valor que puede ser útil en algunos casos de uso

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
