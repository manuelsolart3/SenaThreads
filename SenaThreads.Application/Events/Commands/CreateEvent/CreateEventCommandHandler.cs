using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Events.Commands.CreateEvent;
public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAwsS3Service _awsS3Service;

    public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IAwsS3Service awsS3Service)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result>Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        //inicializamos vacia por sino hay un valor
        string imageUrl = string.Empty;

        if(request.Image != null)
        {
            imageUrl = await _awsS3Service.UploadFileToS3Async(request.Image);
        }

        //Crear nuevo evento
        Event newEvent = new(
        request.UserId,
        request.Title,
        request.Description ?? string.Empty, //utilizar cadenas vacias por si es nulo
        imageUrl,
        request.EventDate);

        //Agregar el evento al repositorio
        _eventRepository.Add(newEvent);

        // Guardar cambios en la base de datos
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();


    }
}
