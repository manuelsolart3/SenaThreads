using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Application.Tweets.Queries.GetTweetComments;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using static System.Net.Mime.MediaTypeNames;

namespace SenaThreads.Application.Tweets.Queries.GetTweetComment;
public class GetTweetCommentsQueryHandler : IQueryHandler<GetTweetCommentsQuery, Pageable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetTweetCommentsQueryHandler(
        IMapper mapper,
        ICommentRepository commentRepository,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService,
        ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Pageable<CommentDto>>> Handle(GetTweetCommentsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedComments = await FetchData(request.TweetId, request.Page, request.PageSize, currentUserId);

        foreach (var comment in paginatedComments.List)
        {
            if (!string.IsNullOrEmpty(comment.ProfilePictureS3Key))
            {
                comment.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(comment.ProfilePictureS3Key);
            }
        }
        return Result.Success(paginatedComments);
    }

    private async Task<Pageable<CommentDto>> FetchData(Guid tweetId, int page, int pageSize, string currentUserId)
    {
        int start = pageSize * (page - 1);

        IQueryable<Comment> commentsQuery = _commentRepository.Queryable()
            .Where(c => c.TweetId == tweetId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedOnUtc); // Ordenar los comentarios por fecha de creación

        List<Comment> comments = await commentsQuery.ToListAsync();
        List<CommentDto> commentDtos = _mapper.Map<List<CommentDto>>(comments);

        // Aplicar el filtro de bloqueo a los comentarios
        var filteredCommentDtos = await _blockFilterService.FilterBlockedContent(commentDtos, currentUserId, c => c.UserId);

        // Calcular el total de comentarios filtrados
        int totalFilteredCount = filteredCommentDtos.Count();

        // Aplicar paginación después del filtrado
        var paginatedCommentDtos = filteredCommentDtos
            .Skip(start)
            .Take(pageSize)
            .ToList();

        // Retornar nuevo Objeto pageable con la lista de Dtos y el total de comentarios
        return new Pageable<CommentDto>
        {
            List = paginatedCommentDtos,
            Count = totalFilteredCount
        };
    }
}


