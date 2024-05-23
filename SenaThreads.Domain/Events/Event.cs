using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Events;

public class Event : Entity
{
    
    public Guid Id { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public DateTime EventDate { get; set; }
    public DateTime EventHour { get; set; }

    

    public Event(string userId, string title, string description, string image, DateTime eventDate, DateTime eventHour)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Description = description;
        Image = image;
        EventDate = eventDate;
        EventHour = eventHour;
    }
}
