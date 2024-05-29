using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public class AddCommentToTweetCommandHandler : ICommandHandler<AddCommentToTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddCommentToTweetValidator _validator;

    public AddCommentToTweetCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork,
        AddCommentToTweetValidator validator)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(AddCommentToTweetCommand request, CancellationToken cancellationToken)
    {

        //Obtener el tweet al que se requiere comentar
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.TweetId);
        // Verificar si el Tweet existe en la BD
        if (tweet == null)
        {
            return Result.Failure(Error.None);//no se encontro ningun tweet
        }

        //Agregamos el comentario al Tweet
        //lo agregamos a la  coleccion de comentarios en el Tweet
        tweet.Comments.Add(new Comment( // Agregamos una nueva instancia de Comment con los parametros del command 
            request.TweetId,
            request.UserId,
            request.Text));


        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
