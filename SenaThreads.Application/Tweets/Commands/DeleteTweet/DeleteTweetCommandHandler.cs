using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.DeleteTweet;
public class DeleteTweetCommandHandler : ICommandHandler<DeleteTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTweetCommandHandler(ITweetRepository tweetRepository, IUnitOfWork unitOfWork)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
    {   //Obtener el Tweet que se requiere eliminar en la Bd
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.tweetId);
        // Verificar si el Tweet existe o si el usuario es el creador del tweet en la Bd
        if (tweet == null )
        {
            return Result.Failure(TweetError.NotFound);//no se encontro ningun tweet 
        }

        if (tweet.UserId != request.userId)
        {
            return Result.Failure(TweetError.Unauthorized);//No es el creador
        }

        _tweetRepository.Delete(tweet);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
