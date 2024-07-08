using MediatR;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.VisitProfile;
public record VisitProfileCommand(string VisitingUserId, string VisitedUserId) : ICommand<Unit>;
