using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.VisitProfile;
public class VisitProfileCommandHandler : ICommandHandler<VisitProfileCommand, Unit>
{
    private readonly ISearchUserHistoryRepository _searchUserHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VisitProfileCommandHandler(
        ISearchUserHistoryRepository searchUserHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _searchUserHistoryRepository = searchUserHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(VisitProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.VisitingUserId == request.VisitedUserId)
        {
            return Result.Failure<Unit>(UserError.CannotOwnInformation);
        }

        var searchHistory = new SearchUserHistory(request.VisitingUserId, request.VisitedUserId);

        await _searchUserHistoryRepository.AddAsync(searchHistory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
