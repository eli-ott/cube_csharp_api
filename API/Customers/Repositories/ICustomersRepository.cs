﻿using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Models;
using MonApi.Shared.Repositories;
using MonApi.API.Customers.Repositories;
using MonApi.API.Customers.Services;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Pagination;
using MonApi.API.Customers.Filters;

namespace MonApi.API.Customers.Repositories
{
    public interface ICustomersRepository : IBaseRepository<Customer>
    {
        Task<ReturnCustomerDto?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<ReturnCustomerDto?> FindAsync(int id, CancellationToken cancellationToken = default);
        Task<ReturnCustomerDto?> FindWithPasswordAsync(int id, CancellationToken cancellationToken = default);
        Task<PagedResult<ReturnCustomerDto>> ListAsync(CustomerQueryParameters queryParameters, CancellationToken cancellationToken = default);
    }
}
