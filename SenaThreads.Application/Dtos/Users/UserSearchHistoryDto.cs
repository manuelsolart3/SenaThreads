namespace SenaThreads.Application.Dtos.Users;
public class UserSearchHistoryDto
{
    public int Id { get; set; }
    public string SearchedUserId { get; set; }
    public DateTime SearchedAt { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureS3Key { get; set; }
}
