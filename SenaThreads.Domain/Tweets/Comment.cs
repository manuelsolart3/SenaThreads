using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Tweets;

public class Comment : Entity
{
    public Guid Id { get; private set; }
    public Tweet Tweet { get; private set; }
    public Guid TweetId { get; private set; }
    public User User { get; private set; }
    public string UserId { get; private set; }
    public string Text { get; private set; }

    public Comment(Guid tweetId, string userId, string text)
    {
        TweetId = tweetId;
        UserId = userId;
        Text = text;
        Id = Guid.NewGuid();
        CreatedBy = UserId;
        UpdatedBy = UserId;
    }
}
