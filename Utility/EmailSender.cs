using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioProject.Utility
{
    public class EmailSender : IEmailSender 
    {
        public string emails { get; set; }
        public string password { get; set; }
    
        public EmailSender(IConfiguration _config)
        {
            emails = _config["EmailSending:Email"];
            password = _config["EmailSending:Password"];
        }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(emails, password)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: email,
                    to: email,
                    subject,
                    body
                    ));
        }
    }
}
