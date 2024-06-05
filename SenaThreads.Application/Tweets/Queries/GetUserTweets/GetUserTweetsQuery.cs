using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public record GetUserTweetsQuery(string UserId) : IQuery<List<BasicTweetInfoDto>>;
