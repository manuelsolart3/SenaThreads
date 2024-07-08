using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Users.UserQueries.GetUserSearchHistory;
public class GetUserSearchHistoryQueryHandler : IQueryHandler<GetUserSearchHistoryQuery, List<UserSearchHistoryDto>>
{
    private readonly ISearchUserHistoryRepository _searchUserHistoryRepository;
    private readonly IMapper _mapper;
    

    public GetUserSearchHistoryQueryHandler(ISearchUserHistoryRepository searchUserHistoryRepository, IMapper mapper)
    {
        _searchUserHistoryRepository = searchUserHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<UserSearchHistoryDto>>> Handle(GetUserSearchHistoryQuery request, CancellationToken cancellationToken)
    {
        var searchHistory = await _searchUserHistoryRepository.Queryable()
            .Where(h => h.UserId == request.UserId)
            .OrderByDescending(h => h.SearchedAt)
            .Take(request.Limit)
            .Select(h => new UserSearchHistoryDto
            {
                Id = h.Id,
                SearchedUserId = h.SearchedUserId,
                SearchedAt = h.SearchedAt
            })
            .ToListAsync(cancellationToken);

        return Result.Success(searchHistory);
    }
}

