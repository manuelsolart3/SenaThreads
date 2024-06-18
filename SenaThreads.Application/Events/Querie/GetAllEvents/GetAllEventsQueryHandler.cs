using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Events.Querie.GetAllEvents;
public class GetAllEventsQueryHandler : IQueryHandler<GetAllEventsQuery, Pageable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetAllEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<Pageable<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {

        var paginatedEvent = await FetchData(request.Page, request.PageSize);
        return Result.Success(paginatedEvent);


    }
    private async Task<Pageable<EventDto>> FetchData(int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<Event> eventQuery = _eventRepository.Queryable()
            .Include(e => e.User)
            .OrderByDescending(c => c.CreatedOnUtc);




        int totalCount = await eventQuery.CountAsync();

        List<Event> events = await eventQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();


        List<EventDto> eventDtos = _mapper.Map<List<EventDto>>(events);


        //Retorna objeto con la lista de Dtos y el total eventos
        return new Pageable<EventDto>
        {
            List = eventDtos,
            Count = totalCount
        };
    }
}
