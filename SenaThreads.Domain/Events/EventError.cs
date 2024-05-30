using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Events;
public static class EventError
{
    public static readonly Error NotFound = new(
       "Event.Found",
       "The specific Event was not found");

    public static readonly Error Unauthorized = new(
        "Event.Unauthorized",
        "The user is not the creator");
}
