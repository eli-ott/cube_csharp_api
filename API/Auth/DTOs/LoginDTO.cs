using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Auth.DTOs
{
    public class LoginDTO
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}