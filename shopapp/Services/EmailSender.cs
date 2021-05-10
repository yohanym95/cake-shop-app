using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace shopapp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sendGridKey = "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";
            return Execute(sendGridKey, subject, htmlMessage, email);
        }


        public async Task Execute(string apiKey, string subject1, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("malshikay@gmail.com", "EpicLabs via SendGrid"),
                Subject = subject1,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress("malshikay95@gmail.com"));

            //var from = new EmailAddress("malshikay@gmail.com", "Yohan Malshika");
            //var subject = subject1;
            //var to = new EmailAddress("malshikay95@gmail.com", "Yohan Malshika");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            //// Disable click tracking.
            //// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                var errorMessage = response.Body.ReadAsStringAsync().Result;
                throw new Exception($"Failed to send mail to , status code {response.StatusCode}, {errorMessage}");
            }
        }
    }
}
