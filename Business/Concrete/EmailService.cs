using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmailService
    {
        public async Task SendVerificationEmail(string email, string token)
        {
            var verificationUrl = $"http://localhost:4200/email-verify/{token}";
            var message = new EmailMessage()
            {
                Subject = "Email Confirmation",
                Body = $"Lütfen e-posta adresinizi doğrulamak için <a href='{verificationUrl}'>bu bağlantıya</a> tıklayın.",
                To = email
            };

            // Burada SMTP ya da e-posta sağlayıcınızı kullanarak e-posta gönderimini yapabilirsiniz.
            Console.WriteLine($"Doğrulama e-postası gönderildi: {message}");

            var emailSend = new MimeMessage();
            emailSend.From.Add(MailboxAddress.Parse("rabiasurmeli54@gmail.com"));
            emailSend.To.Add(MailboxAddress.Parse(message.To));
            emailSend.Subject = message.Subject;

            // HTML veya düz metin içeriği iletinin içine eklenebilir
            var bodyBuilder = new BodyBuilder { HtmlBody = message.Body };
            emailSend.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // SMTP sunucusuna bağlanıyoruz
                    await smtpClient.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Giriş bilgilerinizi giriyoruz
                    await smtpClient.AuthenticateAsync("rabiasurmeli54@gmail.com", "egyb wawp cmbx xahs");

                    // E-postayı gönderiyoruz
                    await smtpClient.SendAsync(emailSend);
                }
                finally
                {
                    // Bağlantıyı kapatıyoruz
                    smtpClient.DisconnectAsync(true);
                }
            }
        }
    }

    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}
