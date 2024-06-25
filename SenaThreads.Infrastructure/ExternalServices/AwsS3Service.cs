using System.Reflection.Metadata.Ecma335;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DotNetEnv;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SenaThreads.Application.ExternalServices;

namespace SenaThreads.Infrastructure.ExternalServices;

public class AwsS3Service : IAwsS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public AwsS3Service(/*IConfiguration configuration*/)
    {
        var _accessKeyId = Env.GetString("ACCESS_KEY_ID");
        var _secretAccessKey = Env.GetString("SECRET_ACCESS_KEY");
        var region = Env.GetString("REGION");

        _s3Client = new AmazonS3Client(_accessKeyId, _secretAccessKey, RegionEndpoint.GetBySystemName(region));
        _bucketName = Env.GetString("BUCKET");
    }

    public async Task<string> UploadFileToS3Async(IFormFile formFile)
    {
        // Se crea un stream (flujo de datos) en base al archivo 
        Stream fileStream = formFile.OpenReadStream();

        // Obtenemos el nombre del archivo cargado --> ManuelRosero.jpg --> ManuelRosero
        string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);

        // Creamos una key ÚNICA para almacenar el archivo en la S3 de AWS
        var key = $"{Guid.NewGuid()}-{fileName}";

        // Creamos el objeto que necesita AWS S3 para almacenar el archivo cargado
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = fileStream,
            ContentType = formFile.ContentType
        };

        // Mandamos a guardar el archivo a la S3
        await _s3Client.PutObjectAsync(request);

        // Retormanos la key ÚNICA del archivo guardado
        return key;
    }
    
    public async Task<MemoryStream> GetFileFromS3Async(string key)
    {
        try
        {
            if (key.IsNullOrEmpty())
            {
                return null;
            };
            
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
            };

            using var response = await _s3Client.GetObjectAsync(getObjectRequest);
            
            if (response is null)
            {
                return null;
            };

            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
        catch (AmazonS3Exception e)
        {
            if (e.ErrorCode == "NoSuchKey")
            {
                return null;
            }
        }

        return null;
    }

    public string GeneratePresignedUrl(string key, double durationInMinutes = 240)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(durationInMinutes)
        };
        return _s3Client.GetPreSignedURL(request);
    }
}
