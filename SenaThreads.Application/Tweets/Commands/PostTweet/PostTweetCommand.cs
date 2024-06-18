using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public record PostTweetCommand(
    string UserId,
    string Text,
    IEnumerable<IFormFile>? Attachments = null) : ICommand;
