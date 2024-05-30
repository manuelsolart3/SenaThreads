using System.Data.Entity;
using Microsoft.AspNet.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UnBlockUser;
public class UnBlockUserCommandHandler : ICommandHandler<UnBlockUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepository<UserBlock> _userBlockRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnBlockUserCommandHandler(UserManager<User> userManager, IRepository<UserBlock> userBlockRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _userBlockRepository = userBlockRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UnBlockUserCommand request, CancellationToken cancellationToken)
    {
        User blockUserId = await _userManager.FindByIdAsync(request.BlockedUserId);
        User blockByUserId = await _userManager.FindByIdAsync(request.BlockByUserId);

        if (blockUserId is null || blockByUserId is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        //Buscar la relacion de bloqueo
        UserBlock block = await _userBlockRepository
            .Queryable()
            .FirstOrDefaultAsync(X => X.BlockByUserId == request.BlockedUserId 
            && X.BlockByUserId == request.BlockByUserId);

        if (block != null)
        { 
         _userBlockRepository.Delete(block);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.RelationNotFound);
        }
    }
}
