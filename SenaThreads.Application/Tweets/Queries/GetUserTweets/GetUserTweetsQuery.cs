using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public record GetUserTweetsQuery(string UserId, int Page, int PageSize) : IQuery<Pageable<BasicTweetInfoDto>>;
