using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Dtos.Notifications;
public class NotificationDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public NotificationType Type { get; set; }
    public string Path { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
