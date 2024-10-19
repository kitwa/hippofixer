using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Constants;
using API.DTOs;

namespace API.Features.Messages.Commands
{
    public class SendMessageEmail
    {
        public async Task<bool> SendMessageEmailAsync(string senderEmail, string recipientEmail, string username)
        {

            string subject = "Vous avez reçu un message!";

            string htmlTemplate;
            using (var reader = File.OpenText("./EmailTemplates/MessageEmailTemplate.html"))
            {
                htmlTemplate = await reader.ReadToEndAsync();
            }

            SmtpClient client = new SmtpClient(MailClientConfigurations.Server)
            {
                Port = MailClientConfigurations.Port,
                Credentials = new NetworkCredential(MailClientConfigurations.SenderEmail, MailClientConfigurations.Password),
                EnableSsl = false,
                UseDefaultCredentials = false
            };

            MailMessage mailMessage = new MailMessage(MailClientConfigurations.SenderEmail, recipientEmail)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = htmlTemplate
            }; 

            mailMessage.Body = mailMessage.Body.Replace("[username]", username);

            try
            {
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
                return false;
            }
        }
    }
}
