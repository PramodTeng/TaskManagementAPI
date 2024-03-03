using Task_Management_API.Model;

namespace Task_Management_API.Interface
{
    public interface IEmailService
    {
        Task SendEmailwithAttachments(Email request, List<string> attachments, List<string> attachmentNames);

    }
}
