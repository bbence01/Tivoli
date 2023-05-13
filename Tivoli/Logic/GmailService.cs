using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.IO;
using System.Threading;
using System.Net.Mail;

public class GmailService
{
    private const string Path = "D:/Egyetem/4.felev/INFOBiz/feleves/client_secret_866062964062-7cs7pttg837m71r6pco9th2v8h0pjhhj.apps.googleusercontent.com.json";
    private static string[] Scopes = { Google.Apis.Gmail.v1.GmailService.Scope.GmailSend };
    private static string ApplicationName = "workgroupmanager";

    public static Google.Apis.Gmail.v1.GmailService GetService()
    {
        UserCredential credential;

        using (var stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            Console.WriteLine("Credential file saved to: " + credPath);

            return new Google.Apis.Gmail.v1.GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
    }

    public static void SendMail(MailMessage mail)
    {
        var service = GetService();
        var mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);
        var msg = new Message
        {
            Raw = EncodeBase64Url(mimeMessage.ToString())
        };
        service.Users.Messages.Send(msg, "me").Execute();
    }

    private static string EncodeBase64Url(string input)
    {
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(inputBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .Replace("=", "");
    }
}
