using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.FollowUser;
public class FollowUserCommandHandler : ICommandHandler<FollowUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IFollowRepository _followRepository;

    public FollowUserCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IFollowRepository followRepository)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _followRepository = followRepository;
    }

    public async Task<Result> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        //Buscar al usuario seguidor y usuario seguido
        User followerUser = await _userManager.FindByIdAsync(request.followerUserId);
        User followedByUserId = await _userManager.FindByIdAsync(request.followedByUserId);

        if (followerUser is null || followedByUserId is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }
        // Verificar si ya existe el seguimiento
        if (await _followRepository.IsFollowing(request.followerUserId, request.followedByUserId))
        {
            return Result.Failure(UserError.AlreadyExists);
        }

        //obejto de seguimiento
        Follow newfollow = new (
            request.followerUserId,
            request.followedByUserId); 
        
        //Agregar el nuevo seguimiento 
        _followRepository.Add(newfollow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
