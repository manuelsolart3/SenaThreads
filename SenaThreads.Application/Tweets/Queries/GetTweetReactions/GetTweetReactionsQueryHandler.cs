using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetReactions;
public class GetTweetReactionsQueryHandler : IQueryHandler<GetTweetReactionsQuery, Pageable<ReactionDto>>
{
    private readonly IReactionRepository _reactionRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetTweetReactionsQueryHandler(IReactionRepository reactionRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService, 
        ICurrentUserService currentUserService)
    {
        _reactionRepository = reactionRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Pageable<ReactionDto>>> Handle(GetTweetReactionsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedReactions = await FetchData(request.tweetId, request.filterType, request.page, request.pageSize, currentUserId);

            foreach (var reaction in paginatedReactions.List)
        {
            if (!string.IsNullOrEmpty(reaction.ProfilePictureS3key))
            {
                reaction.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(reaction.ProfilePictureS3key);
            }
        }
        return Result.Success(paginatedReactions);
    }

    private async Task<Pageable<ReactionDto>> FetchData(Guid tweetId, ReactionType? filterType, int page, int pageSize, string currentUserId)
    {
        int start = pageSize * (page - 1);

        //Consulta donde coincida el tweet y se incluya el user
        IQueryable<Reaction> reactionsQuery = _reactionRepository.Queryable()
            .Where(r => r.TweetId == tweetId)
            .Include(r => r.User);

        if (filterType.HasValue)
        {
            reactionsQuery = reactionsQuery.Where(r => r.Type == filterType.Value);
        }
        //Ordenado por fecha de creacion
        reactionsQuery = reactionsQuery.OrderByDescending(r => r.CreatedOnUtc);

        List<Reaction> reactions = await reactionsQuery.ToListAsync();
        //Mapear reactions al Dto
        List<ReactionDto> reactionDtos = _mapper.Map<List<ReactionDto>>(reactions);

        //Aplicar el filtro de bloqueo de contenido de usuarios bloqueados
        var filteredReactionDtos = await _blockFilterService.FilterBlockedContent(reactionDtos, currentUserId, r => r.UserId);

        //Total de reacciones
        int totalFilteredCount = filteredReactionDtos.Count();

        //Paginacion
        var paginatedReactionDtos = filteredReactionDtos
            .Skip(start)
            .Take(pageSize)
            .ToList();

        //Nuevo objeto pageable con la lista de Dtos y total de reacciones
        return new Pageable<ReactionDto>
        {
            List = paginatedReactionDtos,
            Count = totalFilteredCount
        };
    }
}
