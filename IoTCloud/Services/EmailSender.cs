using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Text;

namespace IoTCloud.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        string _email = "";
        string _password = "";

        public EmailSender(IConfiguration config)
        {
            _config = config;
            SetEmailPassword();
        }

        public void SetEmailPassword()
        {
            _email = _config["EmailSender:Email"]!;
            _password = _config["EmailSender:Password"]!;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            System.Net.NetworkCredential credentials = new(_email, _password);
            var sender = new SmtpSender(() => new SmtpClient("smtp-mail.outlook.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = credentials,
                EnableSsl = true
            });

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
            StringBuilder template = new();
            template.AppendLine("Hey @Model.Email,");
            template.AppendLine("<p>@Model.Message.</p>");
            template.AppendLine("- Developer of IoTCloud");

            var emailToSend = Email
                .From(_email)
                .To(email)
                .Subject(subject)
                .UsingTemplate(template.ToString(), new { Email = email, Message = htmlMessage })
                .Send();
            return Task.CompletedTask;
        }
    }
}
