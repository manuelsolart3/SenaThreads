using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetReactions;
public record GetTweetReactionsQuery
    (Guid tweetId,
    ReactionType? filterType,
    int page,
    int pageSize) : IQuery<Pageable<ReactionDto>>;
