using SnapFile.Application.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SnapFile.Infrastructure.Services
{
    public class EmailService:IEmailService
    {
        public async Task SendCode(string email, string code)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("bolnicka65@gmail.com", "ljqbmqbjrmyxcfkr"),
                EnableSsl = true
            };

            var mail = new MailMessage(
                "your@mail.com",
                email,
                "Код подтверждения",
                $"Ваш код: {code}"
            );

            await client.SendMailAsync(mail);
        }
    }
}
