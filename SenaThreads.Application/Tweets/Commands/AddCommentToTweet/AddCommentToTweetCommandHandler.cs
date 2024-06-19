using MediatR;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.Tweets.Commands.PostTweet;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public class AddCommentToTweetCommandHandler : ICommandHandler<AddCommentToTweetCommand>
{
   private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ITweetRepository _tweetRepository;


    public AddCommentToTweetCommandHandler(

        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ICommentRepository commentRepository,
        ITweetRepository tweetRepository)
    {

        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _commentRepository = commentRepository;
        _tweetRepository = tweetRepository;
    }

    public async Task<Result> Handle(AddCommentToTweetCommand request, CancellationToken cancellationToken)
    {
        User commenter = await _userManager.FindByIdAsync(request.UserId);
        if (commenter is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        //Obtener el tweet al que se requiere comentar
        Tweet tweet = await FetchTweetByIdWithComments(request.TweetId);
        // Verificar si el Tweet existe en la BD
        if (tweet is null)
        {
            return Result.Failure(TweetError.NotFound);//no se encontro ningun tweet
        }

        // Crear el comentario
        var comment = new Comment(
            request.TweetId,
            request.UserId,
            request.Text
        );

        // Agregar el nuevo comentario al repositorio de comentarios
        _commentRepository.Add( comment );

        // Agregar el comentario a la colección de comentarios del tweet 
        tweet.Comments.Add(comment);

        // Guardar los cambios en la base de datos
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Tweet> FetchTweetByIdWithComments (Guid TweetId)
    {
        return await _tweetRepository
            .Queryable()
            .Where(tweet => tweet.Id == TweetId)
            .Include(tweet => tweet.Comments)
            .FirstOrDefaultAsync();
    }
}

