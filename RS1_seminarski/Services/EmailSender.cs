using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity.UI.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "sahir.contact@gmail.com";
            string fromPassword = "123456789Nulla!123456789Nulla!";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            if (subject == "AutoReser - Potvrdite registraciju")
                message.Body = "<html><body> " + "Klikni na link za aktivaciju naloga ---> " + htmlMessage + " </body></html>";
            if(subject == "AutoReser - Potvrdite RESET lozinke")
                message.Body = "<html><body> " + "Klikni na link za RESET lozinke ---> " + htmlMessage + " </body></html>";

            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}

