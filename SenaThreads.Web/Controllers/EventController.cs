using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Application.Events.Commands.CreateEvent;
using SenaThreads.Application.Events.Commands.DeleteEvent;
using SenaThreads.Application.Events.Querie.GetAllEvents;
using SenaThreads.Application.Events.Querie.GetUserEvents;

namespace SenaThreads.Web.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //CREAR UN EVENTO
    [HttpPost("create")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Event created successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //ELIMINAR UN EVENTO
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteEvent([FromBody] DeleteEventCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Event deleted successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //OBTENER TODOS LOS EVENTOS
    [HttpGet("all")]
    public async Task<ActionResult> GetAllEvents([FromQuery] GetAllEventsQuery query, CancellationToken cancellationToken)
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

    //OBTENER EVENTOS DE UN USUARIO
    [HttpGet("user")]
    public async Task<ActionResult> GetUserEvents([FromQuery] GetUserEventsQuery query, CancellationToken cancellationToken)
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
