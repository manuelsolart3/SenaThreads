using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public class PostTweetCommandHandler : ICommandHandler<PostTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PostTweetCommandValidator _validator;

    public PostTweetCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork,
        PostTweetCommandValidator validator)
    {
        _unitOfWork = unitOfWork;
        _tweetRepository = tweetRepository;
        _validator = validator;
    }

    public async Task<Result> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {
        //Validar el comando request usando el validator
        var ValidationResult = _validator.Validate(request);
        if (!ValidationResult.IsValid)
        {
            return Result.Failure(Error.None);
        }

        List<TweetAttachment> tweetAttachments = new();
        
        // TODO: Logica para la creación de los 'attachments' del tweet y guardarlos en un bucket de S3 de AWS

        Tweet newTweet = new(
            request.UserId,
            request.Text,
            tweetAttachments);

        _tweetRepository.Add(newTweet);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
