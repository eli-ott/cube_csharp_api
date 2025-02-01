using MonApi.Shared.Services;
using MonApi.API.Passwords.Models;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Repositories;

namespace MonApi.API.Passwords.Services
{
    public class PasswordService : BaseService<Password>, IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        public PasswordService(IPasswordRepository repository) : base(repository)
        {
            _passwordRepository = repository;
        }
    }
}
