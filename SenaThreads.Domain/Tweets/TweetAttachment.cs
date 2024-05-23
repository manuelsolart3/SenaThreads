using System.ComponentModel.DataAnnotations;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Tweets
{
    public class TweetAttachment : Entity
    {

        public Guid Id { get; set; }
        public Tweet Tweet { get; private set; }
        public Guid TweetId { get; set; }
        public string BlobOrS3Key { get; set; }

        public TweetAttachment(Guid tweetId, string blobOrS3Key)
        {

            TweetId = tweetId;
            BlobOrS3Key = blobOrS3Key;
            Id = Guid.NewGuid();
        }


    }
}
