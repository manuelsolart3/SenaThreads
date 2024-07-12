using Microsoft.IdentityModel.Tokens;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Events.Commands.EditEvent;
public class EditEventCommandHandler : ICommandHandler<EditEventCommand>
{

    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAwsS3Service _awsS3Service;
    public EditEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IAwsS3Service awsS3Service)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result> Handle(EditEventCommand request, CancellationToken cancellationToken)
    {
        //Buscar evento existente
        Event existingEvent = await _eventRepository.FindByIdAsync(request.EventId);

        if (existingEvent is null)
        {
            return Result.Failure(EventError.NotFound);
        }

        if (existingEvent.UserId != request.UserId)
        {
            return Result.Failure(UserError.Unauthorized);
        }

        // Actualizar la imagen si se ha proporcionado una nueva
        if (request.Image != null)
        {
            // Subir la nueva imagen a S3
            string newImageUrl = await _awsS3Service.UploadFileToS3Async(request.Image);

            // Eliminar la imagen anterior si existe
            if (!string.IsNullOrEmpty(existingEvent.Image))
            {
                await _awsS3Service.DeleteFileFromS3Async(existingEvent.Image);
            }

            // Actualizar la URL de la imagen
            existingEvent.Image = newImageUrl;
        }

        // Solo actualizar la descripción si ha cambiado
        if (!string.IsNullOrEmpty(request.Description) && request.Description != existingEvent.Description)
        {
            existingEvent.Description = request.Description;
        }

        // Mantener la fecha del evento si no se ha proporcionado una nueva fecha
        if (request.EventDate == default)
        {
            request = request with { EventDate = existingEvent.EventDate };
        }

        // Solo actualizar la fecha del evento si ha cambiado
        if (request.EventDate != existingEvent.EventDate)
        {
            existingEvent.EventDate = request.EventDate;
        }

        existingEvent.Title = request.Title;
        _eventRepository.Update(existingEvent);

        // Guardar cambios en la base de datos
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
