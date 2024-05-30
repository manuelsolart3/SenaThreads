using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Commands.CreateEvent;
public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        //Crear nuevo evento
        Event newEvent = new(
        request.UserId,
        request.Title,
        request.Description,
        request.Image,
        request.EventDate);

        _eventRepository.Add(newEvent);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
