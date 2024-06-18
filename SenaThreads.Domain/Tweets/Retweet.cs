using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Tweets;

public class Retweet : Entity
{
    public Guid Id { get; private set; }
    public Tweet Tweet { get; private set; }    
    public Guid TweetId { get; private set; }
    public User RetweetedBy { get; private set; }
    public string RetweetedById { get; private set; }
    public string Comment  { get; private set; }

    public Retweet(Guid tweetId,string retweetedById, string comment)
    {
        Id = Guid.NewGuid();
        TweetId = tweetId;
        RetweetedById = retweetedById;
        Comment = comment;
        CreatedBy = RetweetedById;
        UpdatedBy = RetweetedById;
    }
}
