using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.ReactToTweet;

//Representa la intencion del usuario de reaccionar a un tweet
public record ReactToTweetCommand ( 
    Guid tweetId,
    string userId,
    ReactionType type) : ICommand;
    

