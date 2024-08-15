using SenaThreads.Domain.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace SenaThreads.Domain.Tweets;
public static class TweetError
{
    public static readonly Error NotFound = new(
        "Tweet.Found",
        "The specific tweet was not found");

    public static readonly Error Unauthorized = new(
        "Tweet.Unauthorized",
        "The user is not the creator");

    public static readonly Error AlreadyRetweeted = new(
        "Retweet.AlreadyRetweeted",
        "The user has already retweeted this tweet");


    public static readonly Error CommentNotFound = new(
         "Comment.NotFound",
    "The comment does not exist");
    
    public static readonly Error MustContainSomething = new(
         "tweet.MustContainSomething",
    "The tweet must contain text or at least one image");


    
}
