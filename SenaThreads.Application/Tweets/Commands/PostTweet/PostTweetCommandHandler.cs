using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public class PostTweetCommandHandler : ICommandHandler<PostTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
   

    public PostTweetCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork
       )
    {
        _unitOfWork = unitOfWork;
        _tweetRepository = tweetRepository;
    }

    public async Task<Result> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {

        List<TweetAttachment> tweetAttachments = new();


        if (request.Attachments != null)
        {
            // TODO: Lógica para la creación de los 'attachments' del tweet y guardarlos en un bucket de S3 de AWS
        }

        Tweet newTweet = new(
            request.UserId,
            request.Text,
            tweetAttachments);

        _tweetRepository.Add(newTweet);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
