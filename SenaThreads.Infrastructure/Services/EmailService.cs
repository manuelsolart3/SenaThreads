using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SenaThreads.Application.IServices;

namespace SenaThreads.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendPasswordResetEmail(string email, string token)
    {
        // Construir el mensaje MIME para el correo de restablecimiento de contraseña
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SenaThreads", "noreply@senathreads.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Restablecimiento de Contraseña - SenaThreads";

        // Construir el cuerpo del mensaje HTML con el token de restablecimiento
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif; color: #333333; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #4a4a4a;'>Restablecimiento de Contraseña</h2>
                    <p>Estimado usuario de SenaThreads,</p>
                    <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta. Si no has sido tú quien ha solicitado este cambio, por favor ignora este mensaje.</p>
                    <p>Tu código de restablecimiento de contraseña es:</p>
                    <div style='background-color: #f0f0f0; padding: 10px; text-align: center; font-size: 24px; font-weight: bold; letter-spacing: 5px; margin: 20px 0;'>
                        {token}
                    </div>
                    <p>Por favor, utiliza este código en la página de restablecimiento de contraseña para crear una nueva contraseña.</p>
                    <p>Este código expirará en 1 hora por razones de seguridad.</p> 
                    <p>Gracias por usar SenaThreads.</p>
                    <p style='font-style: italic; color: #888888;'>Este es un mensaje automático, por favor no respondas a este correo.</p>
                </body>
                </html>"
        };

        // Establecer el cuerpo del mensaje
        message.Body = bodyBuilder.ToMessageBody();

        // Enviar el correo electrónico utilizando el método genérico SendEmailAsync
        await SendEmailAsync(message);
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Construir el mensaje MIME para el correo genérico
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SenaThreads", "noreply@senathreads.com"));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;

        // Construir el cuerpo del mensaje HTML
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        // Establecer el cuerpo del mensaje
        message.Body = bodyBuilder.ToMessageBody();

        // Enviar el correo electrónico utilizando el método genérico SendEmailAsync
        await SendEmailAsync(message);
    }

    private async Task SendEmailAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            // Conectar al servidor SMTP con las configuraciones proporcionadas
            await client.ConnectAsync(
                _configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:Port"]),
                SecureSocketOptions.StartTls);

            // Autenticar con el servidor SMTP
            await client.AuthenticateAsync(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]);

            // Enviar el mensaje MIME al servidor SMTP
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            // Manejar errores de envío de correo electrónico
            throw new ApplicationException("Error al enviar el correo electrónico", ex);
        }
        finally
        {
            // Desconectar del servidor SMTP
            await client.DisconnectAsync(true);
        }
    }
}


