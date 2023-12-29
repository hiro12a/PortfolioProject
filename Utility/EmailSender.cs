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
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = "thomtest965@gmail.com"; // Email that will be used to send 
            var pw = "nvae fcwl qqob ofgw";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject, 
                    body
                    ));
        }
    }
}
