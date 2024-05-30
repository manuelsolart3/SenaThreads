using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public record GetUserTweetsQuery(
    string UserId) : IQuery<List<Tweet>>;
