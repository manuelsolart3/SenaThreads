﻿using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.IRepositories;

public interface ITweetRepository : IRepository<Tweet>
{
    Task<List<Tweet>> GetAllTweetsAsync(string userId = null, bool retweetsOnly = false);
    Task<List<Tweet>> GetMediaTweetsByUserIdAsync(string userId);
}
