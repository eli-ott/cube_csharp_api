using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Customers.DTOs
{
    public class CustomerRequestPasswordResetDto
    {
        [Required]
        [StringLength(255)]
        public required string Email { get; set; }
    }
}
