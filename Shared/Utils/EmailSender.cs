using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MonApi.Shared.Utils;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sender = Environment.GetEnvironmentVariable("SMTP_EMAIL_SENDER")
                     ?? throw new KeyNotFoundException("L'email de l'envoyeur est introuvable");
        var senderPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
                             ?? throw new KeyNotFoundException("Le mot de passe pour le SMTP est introuvable");

        var client = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(sender, senderPassword)
        };

        return client.SendMailAsync(sender, email, subject, htmlMessage);
    }
}