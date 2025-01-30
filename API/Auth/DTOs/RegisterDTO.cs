using MonApi.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Auth.DTOs;

public class RegisterDTO
{
    [StringLength(255)]
    public string FirstName { get; set; }

    [StringLength(255)]
    public string LastName { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [PasswordValidator]
    public string Password { get; set; }
}
