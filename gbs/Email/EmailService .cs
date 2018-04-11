
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public bool Send(EmailMessage emailMessage)
        {
            bool successfullySent = false;
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(emailMessage.FirstName, emailMessage.ToAddresses)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(emailMessage.FirstName, emailMessage.FromAddresses)));

            message.Subject = emailMessage.Subject + " coming through GBS site contact us submit form.";
            //We will say we are sending HTML. But there are options for plaintext etc. 
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = "From email: " + emailMessage.FromAddresses  + Environment.NewLine +
                "Last name: " + emailMessage.LastName + Environment.NewLine +
                "First name: " + emailMessage.FirstName + Environment.NewLine +
                "Phone number: " + emailMessage.phoneNumber + Environment.NewLine +
                "Content: " + emailMessage.Content
            };

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                //The last parameter here is to use SSL (Which you should!)
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                emailClient.Send(message);
                successfullySent = true;
                emailClient.Disconnect(true);
            }
            return successfullySent;
        }

        //public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        //{
        //    using (var emailClient = new Pop3Client())
        //    {
        //        emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

        //        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

        //        emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

        //        List<EmailMessage> emails = new List<EmailMessage>();
        //        for (int i = 0; i < emailClient.Count && i < maxCount; i++)
        //        {
        //            var message = emailClient.GetMessage(i);
        //            var emailMessage = new EmailMessage
        //            {
        //                Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
        //                Subject = message.Subject
        //            };
        //            emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
        //            emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
        //        }

        //        return emails;
        //    }

        //}
    }
}
