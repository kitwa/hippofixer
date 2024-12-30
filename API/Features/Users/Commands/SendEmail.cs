using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Constants;
using API.DTOs;

namespace API.Features.Users.Commands
{
    public class SendEmail
    {
        public async Task SendWelcomeEmailAsync(string recipientEmail, string username)
        {

            // var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); 

            string subject = "Merci d'avoir créé un compte!";

            string htmlTemplate;
            using (var reader = File.OpenText("./EmailTemplates/RegisterEmailTemplate.html"))
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
                client.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        public async Task<bool> SendResetPasswordEmailAsync(string recipientEmail, string username, string link)
        {

            // var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); 

            string subject = "Réinitialiser le mot de passe!";
            string htmlTemplate;
            using (var reader = File.OpenText("./EmailTemplates/ResetPasswordEmailTemplate.html"))
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


            mailMessage.Body = mailMessage.Body.Replace("[callbackUrl]", link);
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

        public async Task<bool> SendEmailWithAttachment(EmailInvoiceDto emailInvoiceDto, string filePath)
        {
            try
            {
                // Save the file temporarily
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await emailInvoiceDto.File.CopyToAsync(stream);
                }

                string subject = "Invoice!";
                string htmlTemplate;
                using (var reader = File.OpenText("./EmailTemplates/InvoiceEmailTemplate.html"))
                {
                    htmlTemplate = await reader.ReadToEndAsync();
                }

                SmtpClient smtpClient = new SmtpClient(MailClientConfigurations.Server)
                {
                    Port = MailClientConfigurations.Port,
                    Credentials = new NetworkCredential(MailClientConfigurations.SenderEmail, MailClientConfigurations.Password),
                    EnableSsl = false,
                    UseDefaultCredentials = false
                };

                MailMessage mailMessage = new MailMessage(MailClientConfigurations.SenderEmail, emailInvoiceDto.Email)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = htmlTemplate
                };

                
                mailMessage.Body = mailMessage.Body.Replace("[InvoiceNumber]", emailInvoiceDto.InvoiceId);
                mailMessage.Body = mailMessage.Body.Replace("[InvoiceDate]", emailInvoiceDto.CreatedDate.ToString("dd/MM/yyyy"));
                mailMessage.Body = mailMessage.Body.Replace("[DueDate]", emailInvoiceDto.DueDate?.ToString("dd/MM/yyyy"));
                mailMessage.Body = mailMessage.Body.Replace("[InvoiceLink]", emailInvoiceDto.InvoiceLink);

                var attachment = new Attachment(filePath);
                mailMessage.Attachments.Add(attachment);

                smtpClient.Send(mailMessage);

                attachment.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
