using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;

namespace SenaThreads.Application.Events.Querie.GetEventById;
public record GetEventByIdQuery(Guid eventId): IQuery<EventDto>;
