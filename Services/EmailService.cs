using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using HospitalManagementSystem.Models;
using System.Net;

namespace HospitalManagementSystem.Services;

public class EmailService : IEmailService
{
    private const string templatePath = @"EmailTemplate/{0}.html";
    private readonly SMTPConfigModel _smtpConfig;

    public async Task SendResetPasswordEmail(UserEmailOptions userEmailOptions)
    {
        userEmailOptions.Subject = UpdatePlaceHolders("Reset password for {{username}}", userEmailOptions.PlaceHolders);
        userEmailOptions.Body = UpdatePlaceHolders(GetEmail("ResetPasswordEmail"), userEmailOptions.PlaceHolders);
        await SendEmail(userEmailOptions);
    }
    public EmailService(IOptions<SMTPConfigModel> smtpConfig)
    {
        _smtpConfig = smtpConfig.Value;
    }
    private async Task SendEmail(UserEmailOptions userEmailOptions)
    {
        try
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            mail.To.Add(userEmailOptions.toEmail);

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = System.Text.Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
        catch (Exception e)
        {
            Console.Write(e);
        }
    }

    private string GetEmail(string name)
    {
        var body = File.ReadAllText(string.Format(templatePath, name));
        return body;
    }

    private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
    {
        if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
        {
            foreach (var p in keyValuePairs)
            {
                if (text.Contains(p.Key))
                {
                    text = text.Replace(p.Key, p.Value);
                }
            }
        }
        return text;
    }
}