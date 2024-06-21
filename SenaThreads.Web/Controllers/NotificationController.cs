using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Notifications.Commands.MarkNotificationAsRead;
using SenaThreads.Application.Notifications.Commands.SendNotification;
using SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;

namespace SenaThreads.Web.Controllers;

[Route("api/notifications")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ENVIAR NOTIFICACION
    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Notification sent successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    // MARCAR NOTIFICACION COMO LEIDA
    [HttpPut("mark-as-read")]
    public async Task<IActionResult> MarkNotificationAsRead([FromBody] MarkNotificationAsReadCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Notification marked as read successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //OBTENER NOTIFACIONES DE UN USER
    [HttpGet("user")]
    public async Task<IActionResult> GetUserNotification([FromQuery]GetUserNotificationsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }
}


