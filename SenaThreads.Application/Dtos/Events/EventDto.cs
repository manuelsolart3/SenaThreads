namespace SenaThreads.Application.Dtos.Events;
public class EventDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public DateTime EventDate { get; set; }
    public DateTime CreatedOnUtc { get; set; }

}
