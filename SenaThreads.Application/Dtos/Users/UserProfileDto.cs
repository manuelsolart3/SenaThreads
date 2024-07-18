namespace SenaThreads.Application.Dtos.Users;
public class UserProfileDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public  string PhoneNumber { get; set; }
    public string Biography { get; set; }
    public string ProfilePictureS3key { get; set; }
    public string BlockType { get; set; }
}
