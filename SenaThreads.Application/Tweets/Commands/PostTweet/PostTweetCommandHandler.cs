using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;

public class PostTweetCommandHandler : ICommandHandler<PostTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAwsS3Service _awsS3Service;

    public PostTweetCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork, IAwsS3Service awsS3Service)
    {
        _unitOfWork = unitOfWork;
        _awsS3Service = awsS3Service;
        _tweetRepository = tweetRepository;
    }

    public async Task<Result> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {
        List<TweetAttachment> tweetAttachments = new();

        Tweet newTweet = new(
            request.UserId,
            request.Text,
            tweetAttachments);
        
        await UploadAttachments(request, newTweet, tweetAttachments);

        newTweet.Attachments = tweetAttachments;

        _tweetRepository.Add(newTweet);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task UploadAttachments(PostTweetCommand request, Tweet newTweet, List<TweetAttachment> tweetAttachments)
    {
        // TODO: Lógica para la creación de los 'attachments' del tweet y guardarlos en un bucket de S3 de AWS
        if (request.Attachments != null)
        {
            foreach (var attachment in request.Attachments)
            {
                var key = await _awsS3Service.UploadFileToS3Async(attachment);
                
                var tweetAttachment = new TweetAttachment(key);
                
                tweetAttachments.Add(tweetAttachment);
            }
        }
    }
}
