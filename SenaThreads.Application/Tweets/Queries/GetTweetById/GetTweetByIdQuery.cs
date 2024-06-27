using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetByIdQuery;
public record GetTweetByIdQuery(Guid TweetId) : IQuery<BasicTweetInfoDto>;
