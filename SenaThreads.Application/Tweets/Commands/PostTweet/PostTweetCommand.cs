using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public record PostTweetCommand(
    string userId,
    string? text,
    IEnumerable<IFormFile>? attachments = null) : ICommand;
