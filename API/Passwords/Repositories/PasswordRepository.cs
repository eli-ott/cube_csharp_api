using MonApi.Shared.Data;
using MonApi.Shared.Repositories;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Passwords.Repositories
{
    public class PasswordRepository : BaseRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
