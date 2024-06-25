using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Querie.GetUserEvents;
public class GetUserEventsQueryHandler : IQueryHandler<GetUserEventsQuery, Pageable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetUserEventsQueryHandler(
        IEventRepository eventRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<Pageable<EventDto>>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
    {

        var paginatedEvents = await FetchData(request.UserId, request.Page, request.PageSize);

        foreach (var eventDto in paginatedEvents.List)
        {
            if (!string.IsNullOrEmpty(eventDto.Image))
            {
                eventDto.Image = _awsS3Service.GeneratePresignedUrl(eventDto.Image);
            }
        }

        return Result.Success(paginatedEvents);
    }

    public async Task<Pageable<EventDto>> FetchData (string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        //Construuir la consulta
        IQueryable<Event> eventQuery = _eventRepository.Queryable()
            .Include(e => e.User)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.CreatedOnUtc);

        int totalCount = await eventQuery.CountAsync();

        List<Event> pagedEvent = await eventQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        List<EventDto> userEventDtos = _mapper.Map<List<EventDto>>(pagedEvent);

        return new Pageable<EventDto>
        {
            List = userEventDtos,
            Count = totalCount
        };

    }
}
