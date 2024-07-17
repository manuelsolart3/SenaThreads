using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.RemoveReaction;
public class RemoveReactionCommandHandler : ICommandHandler<RemoveReactionCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReactionRepository _reactionRepository;

    public RemoveReactionCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork,
        IReactionRepository reactionRepository)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
        _reactionRepository = reactionRepository;
    }

    public async Task<Result> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
    {
        // Obtener el tweet
        var tweet = await _tweetRepository.Queryable()
            .Include(t => t.Reactions)
            .FirstOrDefaultAsync(t => t.Id == request.tweetId, cancellationToken);

        if (tweet is null)
        {
            return Result.Failure(TweetError.NotFound);
        }

        // Buscar la reacción del usuario
        var reaction = tweet.Reactions.FirstOrDefault(r => r.UserId == request.userId);

        if (reaction is null)
        {
            return Result.Failure(TweetError.NotFound);
        }

        // Eliminar la reacción
        tweet.Reactions.Remove(reaction);
        _reactionRepository.Delete(reaction);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
