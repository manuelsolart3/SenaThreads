using System.ComponentModel.DataAnnotations;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Post
{
    public class Comment : Entity
    {
        
        public Guid Id { get; set; }
        public Tweet Tweet { get; private set; }
        public Guid TweetId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }

        public Comment(Guid tweetId, string userId, string text)
        {
            TweetId = tweetId;
            UserId = userId;
            Text = text;
            Id = Guid.NewGuid();
        }
    }
}
