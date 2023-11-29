using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using Final.Project.BL;
using SendGrid;
using SendGrid.Helpers.Mail;
using Final.Project.API.Responses;

namespace Final.Project.API;

public class MailingService : IMailingService
{
    private readonly MailSetting mailSetting;

    public MailingService(IOptions<MailSetting> _mailSetting)
    {
        this.mailSetting = _mailSetting.Value;
    }
    public async Task<UserManagerResponse> SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            MailMessage message = new()
            {
                From = new MailAddress(mailSetting.Email),
                Subject = subject,
                Body = $"<html><body>{body}</body></html>",
                IsBodyHtml = true
            };
            message.To.Add(email);

            var smtpClient = new System.Net.Mail.SmtpClient(mailSetting.Host)
            {
                Port = mailSetting.Port,
                Credentials = new NetworkCredential(mailSetting.Email, mailSetting.Password),
                EnableSsl = mailSetting.EnableSsl,
            };

            await smtpClient.SendMailAsync(message);
            return new UserManagerResponse
            {
                Message = "Email Send Successfully!",
                IsSuccess = true,
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                 IsSuccess = false,
                 Message = ex.Message
            };
        }
    }


}
