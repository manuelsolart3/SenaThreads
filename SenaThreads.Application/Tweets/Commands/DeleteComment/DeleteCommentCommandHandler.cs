using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(ITweetRepository tweetRepository, IUnitOfWork unitOfWork)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        // Verificar si el Tweet existe en la base de datos
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.tweetId);
        if (tweet is null)
        {
            return Result.Failure(TweetError.NotFound); //No se encontro el Tweet
        }
        // Verificar si el Comentario existe en el tweet
        Comment comment = tweet.Comments.FirstOrDefault(c => c.Id == request.commentId);
        if (comment is null)
        {
            return Result.Failure(TweetError.CommentNotFound); // No se encontró el comentario
        }

        // Verificar si el usuario es el creador del comentario
        if (comment.UserId != request.userId)
        {
            return Result.Failure(TweetError.Unauthorized); // El usuario no es el creador del comentario
        }

        tweet.Comments.Remove(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();    
    }
}
