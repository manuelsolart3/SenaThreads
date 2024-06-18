using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Events.Commands.DeleteEvent;
using SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
using SenaThreads.Application.Tweets.Commands.DeleteTweet;
using SenaThreads.Application.Tweets.Commands.PostTweet;
using SenaThreads.Application.Tweets.Commands.ReactToTweet;
using SenaThreads.Application.Tweets.Queries.GetAllTweets;
using SenaThreads.Application.Tweets.Queries.GetTweetComments;
using SenaThreads.Application.Tweets.Queries.GetUserTweets;
using SenaThreads.Application.Users.UserQueries.GetUserFollowers;

namespace SenaThreads.Web.Controllers;

[Route("api/tweets")] 
[ApiController] 

public class TweetController : ControllerBase //proporciona funcionalidades
{
    private readonly IMediator _mediator;

    public TweetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //CREAR TWEET
    [HttpPost("post")]
    public async Task<IActionResult> PostTweet([FromBody] PostTweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            
            return Ok("Tweet created succesfully");
        }
        {
            return BadRequest(result.Error);
        }
        
    }

    //AÑADIR COMENTARIO A UN TWEET
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddCommentToTweet([FromBody] AddCommentToTweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Comment added successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    // REACCIONAR A UN TWEET
    [HttpPost("react")]
    public async Task<IActionResult> ReactToTweet([FromBody] ReactToTweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Reaction to tweet successful");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //ELIMINAR UN Tweet
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteTweet([FromBody] DeleteTweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok("Tweet deleted successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //OBTENER TODOS LOS TWEETS
    [HttpGet("all")]
    public async Task<IActionResult> GetAllTweets([FromQuery] GetAllTweetsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //OBTENER TODOS LOS TWEETS DE UN USUARIO
    [HttpGet("user")]
    public async Task<IActionResult> GetUserTweets([FromQuery] GetUserTweetsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //OBTENER TODOS LOS COMENTARIOS DE UN TWEET
    [HttpGet("comments")]
    public async Task<IActionResult> GetTweetComments([FromQuery] GetTweetCommentsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
