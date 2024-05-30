using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UnFollowUser;
public class UnfollowUserCommandHandler : ICommandHandler<UnfollowUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IFollowRepository _followRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfollowUserCommandHandler(UserManager<User> userManager, IFollowRepository followRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _followRepository = followRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        User followerUserId = await _userManager.FindByIdAsync(request.FollowerUserId);
        User followedByUserId = await _userManager.FindByIdAsync(request?.FollowedByUserId);

        if (followerUserId is null || followedByUserId is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        //Buscamos la relacion de seugimiento




        return Result.Success();
    }
}
