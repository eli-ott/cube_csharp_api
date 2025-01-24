using MonApi.API.Customers.Models;
using MonApi.Shared.Repositories;
using MonApi.API.Customers.Repositories;
using MonApi.API.Customers.Services;

namespace MonApi.API.Customers.Repositories
{
    public interface ICustomersRepository : IBaseRepository<Customer>
    {
        Task<Customer?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
