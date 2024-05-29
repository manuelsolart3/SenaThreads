using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Tweets;
public static class TweetError
{
    public static readonly Error NotFound = new(
        "Tweet.Found", 
        "The specific tweet was not found");
}
