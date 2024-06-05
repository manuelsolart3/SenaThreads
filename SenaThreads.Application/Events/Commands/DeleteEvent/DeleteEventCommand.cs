using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public record DeleteEventCommand (
    Guid EventId,
    string UserId): ICommand;
