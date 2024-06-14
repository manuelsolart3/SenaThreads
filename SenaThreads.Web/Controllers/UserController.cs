using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Users.Commands.BlockUser;
using SenaThreads.Application.Users.Commands.FollowUser;
using SenaThreads.Application.Users.Commands.LoginUser;
using SenaThreads.Application.Users.Commands.RegisterUser;
using SenaThreads.Application.Users.Commands.UnBlockUser;
using SenaThreads.Application.Users.Commands.UnFollowUser;
using SenaThreads.Application.Users.Commands.UpdateProfile;
using SenaThreads.Application.Users.UserQueries.GetUserFollowed;
using SenaThreads.Application.Users.UserQueries.GetUserFollowers;
using SenaThreads.Application.Users.UserQueries.GetUserProfile;
using SenaThreads.Application.Users.UserQueries.GetUserRegistrationInfo;

namespace SenaThreads.Web.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //REGISTAR USUARIO
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("User registered successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //LOGUEAR USUARIO
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("User Login succesfuly");
        }
        else
        {
            return Unauthorized(result.Error);
        }
    }

    //ACTUALIZAR INFO DE PERFIL
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpadateProfileCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok("Profile Updated succesfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //SEGUIR A UN USUARIO
    [HttpPost("follow")]
    public async Task<IActionResult> FollowUser([FromBody] FollowUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Successfully followed user");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //DEJAR DE SEGUIR A UN USUARIO
    [HttpPost("unfollow")]
    public async Task<IActionResult> UnFollowUser([FromBody] UnfollowUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok("User Unfollow successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //BLOQUEAR A UN USUARIO
    [HttpPost("block")]
    public async Task<IActionResult> BlockUser([FromBody] BlockUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("User blocked successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //DESBLOQUEAR A UN USUARIO
    [HttpPost("unblock")]
    public async Task<IActionResult> UnBlockUser([FromBody] UnBlockUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("User unblocked successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //OBTENER INFO DE PERFIL
    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfile(string userId)
    {
        var query = new GetUserProfileQuery(userId);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return NotFound(result.Error);
        }
    }

    //OBTENER INFO DEl REGISTRO DENTRO DE "EDITAR PERFIL"
    [HttpGet("{userId}/Register-Info")]
    public async Task<IActionResult> GetUserRegistrationInfo(string userId)
    {
        var query = new GetUserRegistrationInfoQuery(userId);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return NotFound(result.Error);
        }
    }

    //OBTENER LISTA DE SEGUIDORES
    [HttpGet("{userId}/followers")]
    public async Task<IActionResult> GetUserFollowers (string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetUserFollowersQuery (userId, page, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return NotFound(result.Error);
        }
    }

    //OBTENER LISTA DE SEGUIDOS
    [HttpGet("{userId}/followed")]
    public async Task<IActionResult> GetUserFollowed(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetUserFollowedQuery(userId, page, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return NotFound(result.Error);
        }
    }

}
