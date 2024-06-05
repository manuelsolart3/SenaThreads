using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserMediaTweets;
public record GetUserMediaTweetsQuery(string UserId) : IQuery<List<BasicTweetInfoDto>>;
