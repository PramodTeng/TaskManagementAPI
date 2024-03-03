using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Interface;
using Task_Management_API.Model;

namespace Task_Management_API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendEmailWithAttachments")]
        public async Task<IActionResult> SendEmailWithAttachments([FromForm] Email request, [FromForm] List<IFormFile> attachments)
        {
            List<string> attachmentPaths = new List<string>();
            List<string> attachmentNames = new List<string>();

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attachment in attachments)
                {
                    if (attachment.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        var fileName = attachment.FileName;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await attachment.CopyToAsync(stream);
                        }
                        attachmentPaths.Add(filePath);
                        attachmentNames.Add(fileName);
                    }
                }
            }

            try
            {
                await _emailService.SendEmailwithAttachments(request, attachmentPaths, attachmentNames);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sending the email: {ex.Message}");
            }
            finally
            {
                foreach (var filePath in attachmentPaths)
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
