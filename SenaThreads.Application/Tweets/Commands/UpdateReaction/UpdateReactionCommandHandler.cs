using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Tweets.Commands.UpdateReaction;
public class UpdateReactionCommandHandler : ICommandHandler<UpdateReactionCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReactionRepository _reactionRepository;
    private readonly UserManager<User> _userManager;

    public UpdateReactionCommandHandler
        (ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork,
        IReactionRepository reactionRepository,
        UserManager<User> userManager)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
        _reactionRepository = reactionRepository;
        _userManager = userManager;
    }

    public async Task<Result> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
    {
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.tweetId);
        if (tweet is null)
        {
            return Result.Failure(TweetError.NotFound); //No se encontro el Tweet
        }

        var userExists = await _userManager.FindByIdAsync(request.userId);
        if (userExists is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        var existingReaction = await _reactionRepository.Queryable()
            .FirstOrDefaultAsync(r => r.TweetId == request.tweetId && r.UserId == request.userId);
        if (existingReaction is null)
        {
            //Si no existe la reaccion crear una nueva
            var newReaction = new Reaction
            (
             request.tweetId,
             request.userId,
             request.newReactionType);

            _reactionRepository.Add(newReaction);
        }
        else
        {
            //si ya existe, actualizar el tipo
            existingReaction.Type = request.newReactionType;
            _reactionRepository.Update(existingReaction);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
