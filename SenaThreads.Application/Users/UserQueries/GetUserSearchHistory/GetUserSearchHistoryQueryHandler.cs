using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserSearchHistory;
public class GetUserSearchHistoryQueryHandler : IQueryHandler<GetUserSearchHistoryQuery, List<UserSearchHistoryDto>>
{
    private readonly ISearchUserHistoryRepository _searchUserHistoryRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly UserManager<User> _userManager;


    public GetUserSearchHistoryQueryHandler(ISearchUserHistoryRepository searchUserHistoryRepository, IMapper mapper, IAwsS3Service awsS3Service, UserManager<User> userManager)
    {
        _searchUserHistoryRepository = searchUserHistoryRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _userManager = userManager;
    }

    public async Task<Result<List<UserSearchHistoryDto>>> Handle(GetUserSearchHistoryQuery request, CancellationToken cancellationToken)
    {
        var searchHistory = await _searchUserHistoryRepository.Queryable()
            .Where(h => h.UserId == request.UserId)
            .OrderByDescending(h => h.SearchedAt)
            .Take(request.Limit)
            .Select(h => new { h.Id, h.SearchedUserId, h.SearchedAt })
            .ToListAsync(cancellationToken);

        var userSearchHistoryDtos = new List<UserSearchHistoryDto>();

        foreach (var history in searchHistory)
        {
            var searchedUser = await _userManager.FindByIdAsync(history.SearchedUserId);
            if (searchedUser != null)
            {
                var dto = new UserSearchHistoryDto
                {
                    Id = history.Id,
                    SearchedUserId = history.SearchedUserId,
                    SearchedAt = history.SearchedAt,
                    Username = searchedUser.UserName,
                    FirstName = searchedUser.FirstName,
                    LastName = searchedUser.LastName,
                    ProfilePictureS3Key = searchedUser.ProfilePictureS3Key
                };

                if (!string.IsNullOrEmpty(dto.ProfilePictureS3Key))
                {
                    dto.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(dto.ProfilePictureS3Key);
                }

                userSearchHistoryDtos.Add(dto);
            }
        }

        return Result.Success(userSearchHistoryDtos);
    }
}

