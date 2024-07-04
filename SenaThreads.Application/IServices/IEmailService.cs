namespace SenaThreads.Application.IServices;
public interface IEmailService
{
    Task SendPasswordResetEmail(string email, string token);
    Task SendEmailAsync(string to, string subject, string body);
}
