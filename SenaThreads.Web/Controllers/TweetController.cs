using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
using SenaThreads.Application.Tweets.Commands.DeleteTweet;
using SenaThreads.Application.Tweets.Commands.PostTweet;
using SenaThreads.Application.Tweets.Commands.ReactToTweet;
using SenaThreads.Application.Tweets.Commands.Retweet;
using SenaThreads.Application.Tweets.Queries.GetAllTweets;
using SenaThreads.Application.Tweets.Queries.GetTweetByIdQuery;
using SenaThreads.Application.Tweets.Queries.GetTweetComments;
using SenaThreads.Application.Tweets.Queries.GetUserMediaTweets;
using SenaThreads.Application.Tweets.Queries.GetUserRetweets;
using SenaThreads.Application.Tweets.Queries.GetUserTweets;

namespace SenaThreads.Web.Controllers;

[Route("api/tweets")] 
[ApiController]
[Authorize]
public class TweetController : ControllerBase //proporciona funcionalidades
{
    private readonly IMediator _mediator;
    

    public TweetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //CREAR TWEET
    [HttpPost("post")]
    public async Task<IActionResult> PostTweet([FromForm] PostTweetCommand command)
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

    //RETWEETEAR UN TWEET
    [HttpPost("retweet")]
    public async Task<IActionResult> Retweet([FromBody] RetweetCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {          
            return Ok("Tweet retweeted successfully");
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //ELIMINAR UN TWEET
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

    //OBTENER TWEETSMEDIA DE UN USUARIO
    [HttpGet("media")]
    public async Task<IActionResult> GetUserMediatweets([FromQuery] GetUserMediaTweetsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //OBTENER TWEET POR SU ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTweetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTweetByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }



    //OBTENER RETWEETS DE UN USUARIO
    [HttpGet("retweets")]
    public async Task<IActionResult> GetUserRetweets([FromQuery] GetUserRetweetsQuery query, CancellationToken cancellationToken)
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
