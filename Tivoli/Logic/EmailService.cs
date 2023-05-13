using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tivoli.Models;
using Azure.Core;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System;
using System.Net;
using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using SendGrid.Helpers.Mail;
using SendGrid;
using SendGrid.Helpers.Mail.Model;

namespace Tivoli.Logic
{
    public class EmailService
    {/*
        internal void SendConfirmationEmail(string emailTo, string confirmationLink)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("tivoliteszt002@gmail.com");
                mail.To.Add(emailTo);
                mail.Subject = "Confirmation Email";
                mail.Body = "<h2>Please confirm your action</h2><a href=\"" + confirmationLink + "\">Confirm</a>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("tivoliteszt002@gmail.com", "TivoliTeszt002*002*");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }
        */
        /*
        internal void SendConfirmationEmail(string toEmail, string confirmationLink)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("tivoliteszt002@yahoo.com");
                message.To.Add(toEmail);
                message.Subject = "Confirm your action";
                message.Body = $"Please confirm your action by clicking the following link: {confirmationLink}";

                using (var client = new SmtpClient("smtp.mail.yahoo.com", 587))
                {
                    client.UseDefaultCredentials = false;

                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("tivoliteszt002@yahoo.com","tivoliteszt002*002*");
                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException ex)
                    {
                        // Log the exception or show a message to the user
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                }
            }
        }
        *//*

        internal void SendConfirmationEmail(string toEmail, string confirmationLink)
        {
            var message = new MailMessage()
            {
                From = new MailAddress("tivoliteszt002@gmail.com"),
                To = { toEmail },
                Subject = "Confirm your action",
                Body = $"Please confirm your action by clicking the following link: {confirmationLink}"
            };

            GmailService.SendMail(message);
        }
        */

        private const string SendGridApiKey = "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";

        public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(SendGridApiKey);
            var from = new EmailAddress(fromEmail);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }





        internal string GenerateConfirmationLink(RequestTivoli request, string action)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("TivoliTeszt")))
            {
                string message = $"{request.Id}/{action}";
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                string token = Convert.ToBase64String(hash);
                return $"myapp://{message}/{token}";
            }
        }


        internal Dictionary<string, ConfirmationAction> confirmationTokens = new Dictionary<string, ConfirmationAction>();

        public class ConfirmationAction
        {
            public int RequestId { get; set; }
            public string Action { get; set; }
        }

    }

}
