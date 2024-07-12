using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Querie.GetUserEvents;
public class GetUserEventsQueryHandler : IQueryHandler<GetUserEventsQuery, Pageable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IUserBlockRepository _userBlockRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBlockFilterService _blockFilterService;

    public GetUserEventsQueryHandler(
        IEventRepository eventRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service,
        IUserBlockRepository userBlockRepository,
        ICurrentUserService currentUserService,
        IBlockFilterService blockFilterService)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _userBlockRepository = userBlockRepository;
        _currentUserService = currentUserService;
        _blockFilterService = blockFilterService;
    }

    public async Task<Result<Pageable<EventDto>>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedEvents = await FetchData(request.UserId, request.Page, request.PageSize, currentUserId);

        foreach (var eventDto in paginatedEvents.List)
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

        return Result.Success(paginatedEvents);
    }

    public async Task<Pageable<EventDto>> FetchData (string userId, int page, int pageSize, string currentUserId)
    {
        if (await _userBlockRepository.IsBlocked(currentUserId, userId) ||
            await _userBlockRepository.IsBlocked(userId, currentUserId))
        {
            return new Pageable<EventDto>
            {
                List = new List<EventDto>(),
                Count = 0,
            };
        }


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

        userEventDtos = (await _blockFilterService.FilterBlockedContent(userEventDtos, currentUserId, e => e.UserId)).ToList();

        return new Pageable<EventDto>
        {
            List = userEventDtos,
            Count = userEventDtos.Count
        };

    }
}
