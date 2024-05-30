using Microsoft.AspNet.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.FollowUser;
public class FollowUserCommandHandler : ICommandHandler<FollowUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Follow> _followRepository;//hacemos uso del repo generico para la entidad follow

    public FollowUserCommandHandler(UserManager<User> userManager, IRepository<Follow> followRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _followRepository = followRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        User followerUser = await _userManager.FindByIdAsync(request.FollowerUserId);
        User followedByUserId = await _userManager.FindByIdAsync(request.FollowedByUserId);

        if (followerUser is null || followedByUserId is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        Follow newfollow = new(
            request.FollowerUserId,
            request.FollowedByUserId); 
        
        _followRepository.Add(newfollow);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
