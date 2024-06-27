using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.Users.Commands.BlockUser;
using SenaThreads.Application.Users.Commands.FollowUser;
using SenaThreads.Application.Users.Commands.LoginUser;
using SenaThreads.Application.Users.Commands.RegisterUser;
using SenaThreads.Application.Users.Commands.UnBlockUser;
using SenaThreads.Application.Users.Commands.UnFollowUser;
using SenaThreads.Application.Users.Commands.UpdateProfile;
using SenaThreads.Application.Users.Commands.UploadProfilePicture;
using SenaThreads.Application.Users.UserQueries.GetUserFollowed;
using SenaThreads.Application.Users.UserQueries.GetUserFollowers;
using SenaThreads.Application.Users.UserQueries.GetUserInfo;
using SenaThreads.Application.Users.UserQueries.GetUserProfile;
using SenaThreads.Application.Users.UserQueries.GetUserRegistrationInfo;
using SenaThreads.Application.Users.UserQueries.GetUsersNotFollowed;
using SenaThreads.Application.Users.UserQueries.SearchUsersByUsername;

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
    [AllowAnonymous]
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
            return Ok(result.Value);
        }
        else
        {
            return Unauthorized(result.Error);
        }
    }



    //ACTUALIZAR INFO DE PERFIL
    [HttpPut("update-profile")]
    [Authorize]
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


    //ACTUALIZAR O SUBIR IMAGEN DE PERFIL
    [HttpPost("uploadPicture")]
    [Authorize]
    public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfilePictureCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok("Upload Profile picture succesfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //SEGUIR A UN USUARIO
    [HttpPost("follow")]
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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

    //BUSCAR USUARIO POR SU USERNAME
    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> SearchUsersByUsername([FromQuery] SearchUsersByUsernameQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //OBTENER INFORMACION PRINCIPAL DEL USUARIO
    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
    {
        var query = new GetUserInfoQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //OBTENER USUARIOS NO SEGUIDOS
    [HttpGet("notfollowed")]
    public async Task<IActionResult> GetUsersNotFollowed([FromQuery] string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetUsersNotFollowedQuery(userId, page, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
