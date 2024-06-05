using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetComments;
public record GetTweetCommentsQuery(
    Guid TweetId): IQuery<List<CommentDto>>;
