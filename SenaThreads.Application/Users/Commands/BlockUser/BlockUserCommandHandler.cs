using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.BlockUser;
public class BlockUserCommandHandler : ICommandHandler<BlockUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserBlockRepository _userBlockRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlockUserCommandHandler(UserManager<User> userManager, IUserBlockRepository userBlockRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _userBlockRepository = userBlockRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        //Buscar al usuario bloqueado y usuario que bloquea
        User blockedUserId = await _userManager.FindByIdAsync(request.BlockedUserId);
        User blockByUserId = await _userManager.FindByIdAsync(request.BlockByUserId);

        //Si no existen retorna usuarios no encontrados
        if (blockedUserId is null || blockByUserId is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        //si ya el usuario bloqueo al otro usuario
        if (await _userBlockRepository.IsBlocked(request.BlockedUserId, request.BlockByUserId))
        {
            return Result.Failure(UserError.AlreadyExists);
        }

        //objeto de nuevo user bloqeuado
        UserBlock newUserBlock = new(
           request.BlockedUserId,
           request.BlockByUserId,
           BlockSatus.Active);

        _userBlockRepository.Add(newUserBlock);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(); 
    }
}
