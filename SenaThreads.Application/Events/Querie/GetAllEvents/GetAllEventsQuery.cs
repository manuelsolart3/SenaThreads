using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Events.Querie.GetAllEvents;
public record GetAllEventsQuery(int page, int pageSize) : IQuery<Pageable<EventDto>>;
