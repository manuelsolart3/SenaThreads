using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Querie.GetUserEvents;
public class GetUserEventsQueryHandler : IQueryHandler<GetUserEventsQuery, List<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetUserEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<EventDto>>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
    {
        List<Event> userEvents = await _eventRepository.GetUserEventsAsync(request.UserId);
        List<EventDto> userEventDtos = _mapper.Map<List<EventDto>>(userEvents);   

        return Result.Success(userEventDtos);
    }
}
