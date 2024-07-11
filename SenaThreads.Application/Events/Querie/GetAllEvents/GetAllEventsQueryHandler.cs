using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Events.Querie.GetAllEvents;
public class GetAllEventsQueryHandler : IQueryHandler<GetAllEventsQuery, Pageable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service  _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetAllEventsQueryHandler(
        IEventRepository eventRepository
        , IMapper mapper,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService,
        ICurrentUserService currentUserService)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Pageable<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedEvent = await FetchData(request.Page, request.PageSize, currentUserId);

        
        if (!string.IsNullOrEmpty(currentUserId))
        {
            paginatedEvent.List = (await _blockFilterService.FilterBlockedContent(paginatedEvent.List, currentUserId, e => e.UserId)).ToList();
        }

        foreach (var eventDto in paginatedEvent.List)
        {
            if (!string.IsNullOrEmpty(eventDto.Image))
            {
                eventDto.Image = _awsS3Service.GeneratePresignedUrl(eventDto.Image);
            }
            if (!string.IsNullOrEmpty(eventDto.ProfilePictureS3Key))
            {
                eventDto.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(eventDto.ProfilePictureS3Key);
            }
        }

        return Result.Success(paginatedEvent);


    }
    private async Task<Pageable<EventDto>> FetchData(int page, int pageSize, string currentUserId)
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


        // Aplicar el filtro de bloqueo
        eventDtos = (await _blockFilterService.FilterBlockedContent(eventDtos, currentUserId, e => e.UserId)).ToList();

        //Retorna objeto con la lista de Dtos y el total eventos
        return new Pageable<EventDto>
        {
            List = eventDtos,
            Count = totalCount
        };
    }
}
