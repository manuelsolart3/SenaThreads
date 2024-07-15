using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        //Obtener el evetno que requiere eliminar
        Event eventDelete = await _eventRepository.GetByIdAsync(request.eventId);
        //Verificar si el evento existe o si el usuario es el creador
        if (eventDelete == null)
        {
            return Result.Failure(EventError.NotFound);//No se encontro el evento 
        }
        if (eventDelete.UserId != request.userId)
        {
            return Result.Failure(EventError.Unauthorized);//No es el creador
        }

        _eventRepository.Delete(eventDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();    
    }
}
