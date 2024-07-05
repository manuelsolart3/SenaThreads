using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.CheckUserBlockStatus;
public class CheckUserBlockStatusQueryHandler : IQueryHandler<CheckUserBlockStatusQuery, UserBlockStatusDto>
{
    private readonly IUserBlockRepository _userBlockRepository;

    public CheckUserBlockStatusQueryHandler(IUserBlockRepository userBlockRepository)
    {
        _userBlockRepository = userBlockRepository;
    }
    public async Task<Result<UserBlockStatusDto>> Handle(CheckUserBlockStatusQuery request, CancellationToken cancellationToken)
    {
        var isBlocked1 = await _userBlockRepository.IsBlocked(request.BlockedUserId, request.BlockByUserId);
        var isBlocked2 = await _userBlockRepository.IsBlocked(request.BlockByUserId, request.BlockedUserId);

        if (isBlocked1 || isBlocked2)
        {
            // Determinar quién es el bloqueado y quién lo bloqueó
            string blockedUserId = isBlocked1 ? request.BlockedUserId : request.BlockByUserId;
            string blockByUserId = isBlocked1 ? request.BlockByUserId : request.BlockedUserId;

            return new UserBlockStatusDto
            {
                IsBlocked = true,
                UsuarioQueHaSidoBloqueado = blockedUserId,
                UsuarioQueLoBloqueo = blockByUserId
            };
        }

        return new UserBlockStatusDto { IsBlocked = false };
    }
}
