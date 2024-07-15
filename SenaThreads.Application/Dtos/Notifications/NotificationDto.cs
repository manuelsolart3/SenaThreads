using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Dtos.Notifications;
public class NotificationDto
{
    
    public Guid NotificationId { get; set; }
    public string  NotifierUserId { get; set; }
    public string NotifierUserName { get; set; }
    public string NotifierFirstName { get; set; }
    public string NotifierLastName { get; set; }
    public NotificationType Type { get; set; }
    public string Path { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string NotifierProfilePictureS3key { get; set; }
}
