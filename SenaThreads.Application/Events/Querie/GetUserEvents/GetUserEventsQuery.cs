using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Events.Querie.GetUserEvents;
public record GetUserEventsQuery(string userId, int page, int pageSize) : IQuery<Pageable<EventDto>>;
