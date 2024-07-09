using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Notifications;

public class Notification : Entity
{
    public Guid Id { get; set; }
    public string ReceiverUserId { get; set; }
    public User Receiver { get; set; }
    public string NotifierUserId { get; set; }
    public NotificationType Type { get; set; }
    public string Path { get; set; } // Ruta de la acción
    public bool IsRead { get; set; } = false; // Indica si la notificación ha sido leída

    public Notification(string receiverUserId, string notifierUserId, NotificationType type, string path)
    {
        Id = Guid.NewGuid();
        ReceiverUserId = receiverUserId;
        NotifierUserId = notifierUserId;
        Type = type;
        Path = path;
        CreatedBy = "system";
        UpdatedBy = "system";
    }

    private Notification() { }
}
