using System.ComponentModel.DataAnnotations;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Post
{
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
        TweetId = tweetId;
            RetweetedById = retweetedById;
            Comment = comment;
            Id = Guid.NewGuid();
        }
    }
}
