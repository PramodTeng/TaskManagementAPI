using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.Model
{
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
