using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.Commands.DeleteSearchHistory;
public class DeleteSearchHistoryCommandHandler : ICommandHandler<DeleteSearchHistoryCommand, Unit>
{
    private readonly ISearchUserHistoryRepository _searchUserHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSearchHistoryCommandHandler(
        ISearchUserHistoryRepository searchUserHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _searchUserHistoryRepository = searchUserHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteSearchHistoryCommand request, CancellationToken cancellationToken)
    {
        await _searchUserHistoryRepository.DeleteAllForUserAsync(request.userId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
