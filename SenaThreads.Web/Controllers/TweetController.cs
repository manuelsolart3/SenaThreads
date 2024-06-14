using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Tweets.Commands.PostTweet;

namespace SenaThreads.Web.Controllers;

[Route("api/tweets")] //Ruta base
[ApiController] //Indica que es un apicontroller

public class TweetController : ControllerBase //proporciona funcionalidades
{
    private readonly IMediator _mediator;

    public TweetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("post")]
    public async Task<IActionResult> PostTweet([FromBody] PostTweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            
            return Ok();
        }
        {
            return BadRequest(result.Error);
        }
        
    }
}
