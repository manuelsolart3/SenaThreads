namespace SenaThreads.Application.Dtos.Tweets;
public class BasicTweetInfoDto
{
    public string UserId { get; set; }
    public string ProfilePictureS3Key { get; set; }
    public Guid TweetId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Text { get; set; }
    public int ReactionsCount { get; set; }
    public int RetweetsCount { get; set; }
    public int CommentsCount { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public ICollection<TweetAttachmentDto> Attachments { get; set; }
}
