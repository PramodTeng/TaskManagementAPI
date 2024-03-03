using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Task_Management_API.Interface;
using Task_Management_API.Model;

namespace Task_Management_API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }



        public async Task SendEmailwithAttachments(Email request, List<string> attachmentPaths, List<string> attachmentNames)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;

                var builder = new BodyBuilder();


                // Set the plain text body using the request body
                builder.TextBody = request.Body;

                // Add attachments
                for (int i = 0; i < attachmentPaths.Count; i++)
                {
                    var attachmentPath = attachmentPaths[i];
                    var attachmentName = attachmentNames[i];

                    if (File.Exists(attachmentPath))
                    {
                        var attachment = new MimePart("application", "pdf")
                        {
                            Content = new MimeContent(File.OpenRead(attachmentPath), ContentEncoding.Default),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Default,
                            FileName = attachmentName // Use the original file name
                        };
                        builder.Attachments.Add(attachment);
                    }
                }

                // Set the email body
                email.Body = builder.ToMessageBody();


                // Send email
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while Sending Emails");
                throw;

            }
        }




    }
}
