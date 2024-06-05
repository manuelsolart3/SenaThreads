using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.Tweets.Queries.GetTweetComments;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetComment;
public class GetTweetCommentsQueryHandler : IQueryHandler<GetTweetCommentsQuery, List<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetTweetCommentsQueryHandler(IMapper mapper, ICommentRepository commentRepository)
    {

        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<Result<List<CommentDto>>> Handle(GetTweetCommentsQuery request, CancellationToken cancellationToken)
    {
        //Obtener todos los comentarios del tweet especificado
        List<Comment> coments = await _commentRepository.GetCommentsByTweetIdAsync(request.TweetId);

        //Mapear los comentarios a Dtos
        List<CommentDto> comenDtos = _mapper.Map<List<CommentDto>>(coments);

        return Result.Success(comenDtos);
    }
}
