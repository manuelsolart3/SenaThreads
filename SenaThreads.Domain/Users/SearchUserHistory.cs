﻿using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users;
public class SearchUserHistory : Entity
{

    public int Id { get; set; }
    public string UserId { get; set; }
    public string SearchedUserId { get; set; }
    public DateTime SearchedAt { get; set; }

    public User User { get; set; }
    public User SearchedUser { get; set; }

    public SearchUserHistory(string userId, string searchedUserId)
    {
        UserId = userId;
        SearchedUserId = searchedUserId;
        SearchedAt = DateTime.UtcNow;
        CreatedBy = userId;
        UpdatedBy = userId;
    }
    public SearchUserHistory()
    {
    }
}
