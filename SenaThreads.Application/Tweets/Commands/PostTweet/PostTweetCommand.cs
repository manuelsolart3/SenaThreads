using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public record PostTweetCommand(
    string UserId,
    string Text,
    IEnumerable<IFormFile> Attachments) : ICommand<Result>;
