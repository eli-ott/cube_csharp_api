using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Models;
using MonApi.Models;

namespace MonApi.API.Passwords.Repositories
{
    public class PasswordRepository : BaseRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(StockManagementContext context) : base(context)
        {
        }
    }
}