using MonApi.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Auth.DTOs;

public class RegisterDTO
{
    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(25)]
    public string PhoneNumber { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [PasswordValidator]
    public string Password { get; set; }

}
