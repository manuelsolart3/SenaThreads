using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Queries.GetUserRetweets;
public record GetUserRetweetsQuery(string userId, int page, int pageSize) : IQuery<Pageable<RetweetDto>>;

