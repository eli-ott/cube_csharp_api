using MonApi.API.Auth.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Passwords.Extensions
{
    public static class PasswordExtensions
    {
        public static Password MapToPasswordModel(this RegisterDTO registerDTO)
        {
            return new Password()
            {
                PasswordId = Guid.NewGuid().ToString(),
                Hash = registerDTO.Password,
                Salt = registerDTO.Password,
                NumberTries = 0
            };
        }
    }
}
