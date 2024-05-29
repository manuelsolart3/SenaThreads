using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public record DeleteEventCommand (
    Guid EventId,
    string UserId): ICommand;
