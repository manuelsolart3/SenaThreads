using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Events.Commands.EditEvent;
public record EditEventCommand(
    Guid EventId,
    string UserId,
    string Title,
    string? Description,
    IFormFile? Image,
    DateTime EventDate) : ICommand;
