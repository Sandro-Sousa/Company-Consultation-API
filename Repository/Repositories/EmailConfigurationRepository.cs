
using Entities.Entites;
using Entities;
using Repository.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Repository.Repositories
{
    public class EmailConfigurationRepository : IEmailConfigurationRepository
    {
        public async Task SendVerificationEmail(User user)
        {
            var fromAddress = new MailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
            var toAddress = new MailAddress(user.Email, user.Name);
            string fromPassword = Configuration.Email.DefaultFromPassword;
            string subject = "Verifique sua conta";
            string body = $"Olá,\n\nSeu código de confirmação é: {user.Email.Verification.Code}\n\nPor favor, use este código para confirmar seu cadastro.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Configuration.Email.DefaultFromEmail, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                try
                {
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Email enviado com sucesso!");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao enviar email: {ex.Message}");
                }
        }
    }
}
