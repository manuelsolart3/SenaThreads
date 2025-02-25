﻿using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public record DeleteCommentCommand(
    Guid tweetId,
    Guid commentId,
    string userId) : ICommand;
