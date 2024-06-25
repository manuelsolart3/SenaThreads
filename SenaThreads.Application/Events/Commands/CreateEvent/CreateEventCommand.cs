using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Events.Commands.CreateEvent;
public record CreateEventCommand(
    string UserId,
    string Title,
    string? Description,
    IFormFile? Image,
    DateTime EventDate) : ICommand;
