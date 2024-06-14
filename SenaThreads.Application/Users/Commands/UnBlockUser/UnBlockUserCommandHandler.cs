using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UnBlockUser;
public class UnBlockUserCommandHandler : ICommandHandler<UnBlockUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserBlockRepository _userBlockRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnBlockUserCommandHandler(UserManager<User> userManager, IUserBlockRepository userBlockRepository, IUnitOfWork unitOfWork)
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
            .FirstOrDefaultAsync(x => x.BlockedUserId == request.BlockedUserId
            && x.BlockByUserId == request.BlockByUserId);

        if (block != null)
        {
            // Eliminar el registro de bloqueo
            _userBlockRepository.Delete(block);

            // Guardar cambios en la base de datos
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.RelationNotFound);
        }
    }
}
