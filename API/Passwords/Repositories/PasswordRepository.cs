using Microsoft.EntityFrameworkCore;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Models;
using MonApi.Shared.Data;

namespace MonApi.API.Passwords.Repositories
{
    public class PasswordRepository : BaseRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(StockManagementContext context) : base(context)
        {
        }
    }
}