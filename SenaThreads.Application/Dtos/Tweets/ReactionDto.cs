using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Dtos.Tweets;
public class ReactionDto
{
    public Guid ReactionId { get; set; }
    public string ProfilePictureS3key { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserId { get; set; }
    public ReactionType Type { get; set; }
}
