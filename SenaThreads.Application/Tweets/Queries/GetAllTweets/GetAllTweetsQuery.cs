using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetAllTweets;
public record GetAllTweetsQuery : IQuery<List<BasicTweetInfoDto>>;
