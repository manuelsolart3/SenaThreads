using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
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
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.TweetId);
        // Verificar si el Tweet existe o si el usuario es el creador del tweet en la Bd
        if (tweet == null || tweet.UserId != request.UserId)
        {
            return Result.Failure(Error.None);//no se encontro ningun tweet o el usuario no es el creador
        }
        //Eliminar Tweet
        _tweetRepository.Delete(tweet);
        //Guardar cambios en la Bd
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
