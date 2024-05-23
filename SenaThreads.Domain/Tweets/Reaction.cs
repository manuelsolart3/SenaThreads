using System.ComponentModel.DataAnnotations;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Tweets
{
    public class Reaction : Entity
    {
       
        public Guid Id { get; private set; }
        public Tweet Tweet { get; private set; }
        public Guid TweetId {  get; private set; }
        public User User { get; set; }
        public string UserId {  get; private set; }

        public ReactionType Type { get; set; }

        public Reaction (Guid id, Guid tweetId, string userId, ReactionType type)
        {

            TweetId = tweetId;
            UserId = userId;
            Type = type;
            Id = Guid.NewGuid();

        }
    }
}
