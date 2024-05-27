using MediatR;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Abstractions.Messaging;

// IQueryHandler<TQuery, TResponse>: Esta interfaz representa un manejador de consultas.
// En el contexto de CQRS, un manejador de consultas es responsable de manejar una consulta específica y devolver el resultado esperado.
// La interfaz está parametrizada por TQuery y TResponse.
// TQuery especifica el tipo de consulta que maneja el manejador, y TResponse especifica el tipo de resultado que devuelve.
// Esta interfaz hereda de IRequestHandler<TQuery, Result<TResponse>>,
// lo que significa que un manejador de consultas es un manejador de solicitudes (request handler) que toma una consulta de tipo TQuery
// y devuelve un resultado de tipo Result<TResponse>.
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
