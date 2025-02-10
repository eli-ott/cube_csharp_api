using MonApi.API.Passwords.Models;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Repositories;

namespace MonApi.API.Passwords.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        public PasswordService(IPasswordRepository repository)
        {
            _passwordRepository = repository;
        }


    }
}
