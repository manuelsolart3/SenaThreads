using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Notifications;

public class Notification : Entity
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Path { get; set; } //Ruta de la accion
    public bool IsRead { get; set; } = false; //indica si la notifiaciones ha sido leida 

    public Notification(string userId, NotificationType type, string path)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Type = type;
        Path = path;
        CreatedBy = "system";
        UpdatedBy = "system";
    }
}
