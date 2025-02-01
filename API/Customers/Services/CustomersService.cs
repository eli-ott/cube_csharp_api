using MonApi.API.Customers.Models;
using MonApi.API.Customers.Repositories;
using MonApi.Shared.Services;

namespace MonApi.API.Customers.Services
{
    public class CustomersService : BaseService<Customer>, ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        public CustomersService(ICustomersRepository customersRepository) : base(customersRepository)
        {
            _customersRepository = customersRepository;
        }
    }
}
