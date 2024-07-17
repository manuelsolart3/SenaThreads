using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Events.Querie.GetEventById;
public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, EventDto>
{

    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserBlockRepository _userBlockRepository;

    public GetEventByIdQueryHandler(
        IEventRepository eventRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService,
        ICurrentUserService currentUserService,
        IUserBlockRepository userBlockRepository)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
        _userBlockRepository = userBlockRepository;
    }

    public async Task<Result<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        var eventEntity = await _eventRepository.Queryable()
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == request.eventId, cancellationToken);

       if (eventEntity is null)
        {
            return Result.Failure<EventDto>(EventError.NotFound);
        }
       if (await _userBlockRepository.IsBlocked (currentUserId, eventEntity.UserId)||
            await _userBlockRepository.IsBlocked(eventEntity.UserId , currentUserId))
        {
            return Result.Failure<EventDto>(UserError.UserBlocked);
        }

       var eventDto = _mapper.Map<EventDto>(eventEntity);

        if (!string.IsNullOrEmpty(eventDto.Image))
        {
            eventDto.Image = _awsS3Service.GeneratepresignedUrl(eventDto.Image);
        }

        if (!string.IsNullOrEmpty(eventDto.ProfilePictureS3key))
        {
            eventDto.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(eventDto.ProfilePictureS3key);
        }

        return Result.Success(eventDto);

    }

}
