using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.ReactToTweet;

//Representa la intencion del usuario de reaccionar a un tweet
public record ReactToTweetCommand ( 
    Guid TweetId,
    string UserId,
    ReactionType Type) : ICommand;
    

