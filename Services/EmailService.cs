using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SITConnect.Settings;

namespace SITConnect.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSetting> _smtpSetting;

        public EmailService(IOptions<SmtpSetting> smtpSetting)
        {
            this._smtpSetting = smtpSetting;
        }

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from,
                    to,
                    subject,
                    body);

            using (var emailClient = new SmtpClient(_smtpSetting.Value.Host, _smtpSetting.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                    _smtpSetting.Value.User,
                    _smtpSetting.Value.Password);

                await emailClient.SendMailAsync(message);
            }
        }
    }
}