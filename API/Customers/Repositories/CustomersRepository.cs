using MonApi.Shared.Repositories;
using MonApi.API.Customers.Models;
using MonApi.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace MonApi.API.Customers.Repositories
{
    public class CustomersRepository : BaseRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.Include(c => c.Password).FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }
    }
}
