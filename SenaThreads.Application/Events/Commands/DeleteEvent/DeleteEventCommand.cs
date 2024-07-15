using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public record DeleteEventCommand (
    Guid eventId,
    string userId): ICommand;
