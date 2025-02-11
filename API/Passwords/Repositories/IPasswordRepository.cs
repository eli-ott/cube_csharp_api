using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Passwords.Repositories
{
    public interface IPasswordRepository : IBaseRepository<Password>
    {
    }
}