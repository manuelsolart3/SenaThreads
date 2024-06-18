using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Queries.GetAllTweets;
public record GetAllTweetsQuery(int Page, int PageSize) : IQuery<Pageable<BasicTweetInfoDto>>;
