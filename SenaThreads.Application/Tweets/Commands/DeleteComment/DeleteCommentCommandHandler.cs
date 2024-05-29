﻿using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(ITweetRepository tweetRepository, IUnitOfWork unitOfWork)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        // Verificar si el Tweet existe en la base de datos
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.TweetId);
        if (tweet == null)
        {
            return Result.Failure(Error.None); //No se encontro el Tweet
        }
        // Verificar si el Comentario existe en el tweet
        Comment comment = tweet.Comments.FirstOrDefault(c => c.Id == request.CommentId);
        if (comment == null || comment.UserId != request.UserId)
        {
            return Result.Failure(Error.None); //no se encontro ningun comment o el usuario no es el creador
        }

        tweet.Comments.Remove(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();    
    }
}
