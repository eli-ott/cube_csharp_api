using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Customers.DTOs
{
    public class LoginDTO
    {
        [Required] [EmailAddress] public required string Email { get; set; }

        [Required] public required string Password { get; set; }
    }
}