using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace ExpenseLog.Services
{
    public class linkSender 
    {
        private readonly IConfiguration _configuration;

        public linkSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            //all of below reference values in appsettings.json
            var smtpServer = _configuration["EmailSettings:SmtpServer"]; //SMTP server used for doing email things
            var port = int.Parse(_configuration["EmailSettings:Port"]); //Is a number; usually 587 or something else
            var senderEmail = _configuration["EmailSettings:SenderEmail"]; //this sends confirmation email
            var password = _configuration["EmailSettings:Password"]; //is found in "app passwords" when doing a search in 'security' in google settings
            var senderName = _configuration["EmailSettings:SenderName"]; //name displayed as email source

            var client = new SmtpClient(smtpServer, port)
            {
                EnableSsl = true, // keeps stuff safe
                Credentials = new NetworkCredential(senderEmail, password) //accepts eamil and password as inputs
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true //allows HTML stuff in email
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}
