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
        // Validar que al menos uno de los campos este presente
        if (string.IsNullOrWhiteSpace(request.text) && (request.attachments == null || !request.attachments.Any()))
        {
            return Result.Failure(TweetError.MustContainSomething);
        }

        List<TweetAttachment> tweetAttachments = new List<TweetAttachment>();

        // Crear el nuevo tweet
        Tweet newTweet = new Tweet(
            request.userId,
            request.text,
            tweetAttachments);

       
        await UploadAttachments(request, newTweet, tweetAttachments);

        // Agregar los adjuntos al tweet
        newTweet.Attachments = tweetAttachments;

        // Agregar el tweet al repositorio
        _tweetRepository.Add(newTweet);

        // Guardar cambios en la base de datos
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    private async Task UploadAttachments(PostTweetCommand request, Tweet newTweet, List<TweetAttachment> tweetAttachments)
    {
        // TODO: Lógica para la creación de los 'attachments' del tweet y guardarlos en un bucket de S3 de AWS
        if (request.attachments != null)
        {
            foreach (var attachment in request.attachments)
            {
                var key = await _awsS3Service.UploadFileToS3Async(attachment);
                
                var tweetAttachment = new TweetAttachment(key)
                {
                    CreatedBy = newTweet.CreatedBy,
                    UpdatedBy = newTweet.UpdatedBy 
                };

                tweetAttachments.Add(tweetAttachment);
            }
        }
    }
}
