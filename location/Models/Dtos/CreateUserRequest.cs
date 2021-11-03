using System.ComponentModel.DataAnnotations;

namespace location.Models.Dtos
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }
    }
}