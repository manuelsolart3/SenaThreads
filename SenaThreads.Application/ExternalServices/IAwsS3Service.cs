using Microsoft.AspNetCore.Http;

namespace SenaThreads.Application.ExternalServices;

public interface IAwsS3Service
{
    Task<string> UploadFileToS3Async(IFormFile formFile);
    Task<MemoryStream> GetFileFromS3Async(string key);
    string GeneratePresignedUrl(string key, double durationInMinutes = 240);
}
