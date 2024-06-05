namespace SenaThreads.Application.Dtos.Tweets;
public class CommentDto
{
    public Guid CommentId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Text { get; set; }
}
