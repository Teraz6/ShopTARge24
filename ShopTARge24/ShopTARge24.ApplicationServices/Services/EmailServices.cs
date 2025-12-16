using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;

        public EmailServices(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = dto.Body
            };

            //failide lisamine
            //kontrollib failide  suurust ja saadab teele

            //tuleb teha foreach tsükkel, kus läbib kõik dto.Attachment failid
            //ja lisab need emailile
            //kui failide arv/suurus on üle mingi piiri siis ei lisa
            if (dto.Attachment != null)
            {
                foreach (var file in dto.Attachment)
                {
                    if (file.Length > 0 && file.Length <= 10 * 1024 * 1024) //limit 10MB
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            ms.Position = 0;
                            //var fileBytes = ms.ToArray();
                            //builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                            builder.Attachments.Add(file.FileName, ms.ToArray());
                        }
                    }
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendEmailToken(EmailTokenDto dto, string token)
        {
            dto.Token = token;
            var email = new MimeMessage();

            _config.GetSection("EmailUserName").Value = "hesyes2020@gmail.com";
            _config.GetSection("EmailHost").Value = "smtp.gmail.com";
            _config.GetSection("EmailPassword").Value = "udsbvwbaffbwcycr";

            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;
            var builder = new BodyBuilder
            {
                HtmlBody = dto.Body,
            };

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}