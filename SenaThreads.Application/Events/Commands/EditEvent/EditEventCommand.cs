using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Events.Commands.EditEvent;
public record EditEventCommand(
    Guid eventId,
    string userId,
    string title,
    string? description,
    IFormFile? image,
    DateTime eventDate) : ICommand;
