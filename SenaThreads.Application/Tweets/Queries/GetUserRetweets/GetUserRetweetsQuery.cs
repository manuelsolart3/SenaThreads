using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserRetweets;
public record GetUserRetweetsQuery(string UserId) : IQuery<List<BasicTweetInfoDto>>;

