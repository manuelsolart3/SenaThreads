namespace SenaThreads.Application.Dtos.Users;
public class UserProfileDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Biography { get; set; }
    public string ProfilePictureS3Key { get; set; }
}
