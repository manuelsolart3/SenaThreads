using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Queries.GetTweetComments;
public record GetTweetCommentsQuery(Guid TweetId, int Page, int PageSize) : IQuery<Pageable<CommentDto>>;
