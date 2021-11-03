using System.ComponentModel.DataAnnotations;

namespace location.Controllers
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }
    }
}