using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;

namespace SenaThreads.Application.Events.Querie.GetUserEvents;
public record GetUserEventsQuery(string UserId) : IQuery<List<EventDto>>;
