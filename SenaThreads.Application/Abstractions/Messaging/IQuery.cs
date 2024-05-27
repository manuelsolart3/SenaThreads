using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Abstractions.Messaging;

// IQuery<TResponse>: Esta interfaz representa una consulta. En el contexto de CQRS,
// una consulta es una operación que solicita datos, pero no los modifica.
// La interfaz está parametrizada por TResponse, que especifica el tipo de datos que se espera que la consulta devuelva.
// En este caso, IQuery<TResponse> hereda de IRequest<Result<TResponse>>,
// lo que significa que una consulta es una solicitud (request) que devuelve un resultado (result) de tipo TResponse.
// Esta interfaz define una operación que se ejecutará para obtener un resultado de tipo TResponse.
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
