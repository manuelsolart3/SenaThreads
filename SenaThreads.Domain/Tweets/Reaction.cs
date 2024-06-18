using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Tweets;

public class Reaction : Entity
{
    public Guid Id { get; private set; }
    public Tweet Tweet { get; private set; }
    public Guid TweetId {  get; private set; }
    public User User { get; private set; }
    public string UserId {  get; private set; }

    public ReactionType Type { get; set; }

    public Reaction(Guid tweetId, string userId, ReactionType type)
    {
        Id = Guid.NewGuid();
        TweetId = tweetId;
        UserId = userId;
        Type = type;
        CreatedBy = UserId;
        UpdatedBy = UserId;
    }
}
