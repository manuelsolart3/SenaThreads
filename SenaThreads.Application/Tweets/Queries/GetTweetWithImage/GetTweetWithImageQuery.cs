using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetWithImage;
public record GetTweetWithImageQuery(
    string UserId):IQuery<List<Tweet>>;
