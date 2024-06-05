using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;

namespace SenaThreads.Application.Events.Querie.GetAllEvents;
public record GetAllEventsQuery : IQuery<List<EventDto>>;
