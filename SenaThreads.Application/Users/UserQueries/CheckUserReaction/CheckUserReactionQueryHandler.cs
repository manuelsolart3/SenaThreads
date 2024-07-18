using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.CheckUserReaction;
public class CheckUserReactionQueryHandler : IQueryHandler<CheckUserReactionQuery, UserReactionCkeckDto>
{
    private readonly IReactionRepository _reactionRepository;

    public CheckUserReactionQueryHandler(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository;
    }

    public async Task<Result<UserReactionCkeckDto>> Handle(CheckUserReactionQuery request, CancellationToken cancellationToken)
    {
        var reaction = await _reactionRepository.Queryable()
            .Where(r => r.TweetId == request.tweetId && r.UserId== request.userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (reaction != null)
        {
           return Result.Success( new UserReactionCkeckDto
            {
                HasReacted = true,
                ReactionType = (int)reaction.Type,
                Message = "A reaction was found for this user in this tweet"
            });
        }
        else
        {
            return Result.Success(
               new UserReactionCkeckDto
               {
                   HasReacted = false,
                   ReactionType = 0,
                   Message = "No reaction was found for this user on this tweet."
               });
        }
            
    }
}
