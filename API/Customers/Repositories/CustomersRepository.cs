using MonApi.Shared.Repositories;
using MonApi.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Data;

namespace MonApi.API.Customers.Repositories
{
    public class CustomersRepository : BaseRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(StockManagementContext context) : base(context)
        {
        }

        public async Task<ReturnCustomerDto?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                where customer.CustomerId == id
                select new ReturnCustomerDto
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Active = customer.Active,
                    CreationTime = customer.CreationTime,
                    UpdateTime = customer.UpdateTime,
                    DeletionTime = customer.DeletionTime,
                    Address = new ReturnAddressDto
                    {
                        AddressId = address.AddressId,
                        AddressLine = address.AddressLine,
                        City = address.City,
                        Country = address.Country,
                        ZipCode = address.ZipCode,
                        Complement = address.Complement,
                        CreationTime = address.CreationTime,
                        UpdateTime = address.UpdateTime,
                        DeletionTime = address.DeletionTime
                    }
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindWithPasswordAsync(int id, CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                join password in _context.Passwords on customer.PasswordId equals password.PasswordId
                where customer.CustomerId == id
                select new ReturnCustomerDto
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Active = customer.Active,
                    CreationTime = customer.CreationTime,
                    UpdateTime = customer.UpdateTime,
                    DeletionTime = customer.DeletionTime,
                    ValidationId = customer.ValidationId,
                    Address = new ReturnAddressDto
                    {
                        AddressId = address.AddressId,
                        AddressLine = address.AddressLine,
                        City = address.City,
                        Country = address.Country,
                        ZipCode = address.ZipCode,
                        Complement = address.Complement,
                        CreationTime = address.CreationTime,
                        UpdateTime = address.UpdateTime,
                        DeletionTime = address.DeletionTime
                    },
                    Password = new ReturnPasswordDto
                    {
                        PasswordId = password.PasswordId,
                        PasswordHash = password.PasswordHash,
                        PasswordSalt = password.PasswordSalt,
                        AttemptCount = password.AttemptCount,
                        UpdateTime = password.UpdateTime,
                        CreationTime = password.CreationTime
                    }
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                    join address in _context.Addresses on customer.AddressId equals address.AddressId
                    join password in _context.Passwords on customer.PasswordId equals password.PasswordId
                    where customer.Email == email
                        select new ReturnCustomerDto
                        {
                            CustomerId = customer.CustomerId,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            Active = customer.Active,
                            CreationTime = customer.CreationTime,
                            UpdateTime = customer.UpdateTime,
                            DeletionTime = customer.DeletionTime,
                            ValidationId = customer.ValidationId,
                            Address = new ReturnAddressDto
                            {
                                AddressId = address.AddressId,
                                AddressLine = address.AddressLine,
                                City = address.City,
                                Country = address.Country,
                                ZipCode = address.ZipCode,
                                Complement = address.Complement,
                                CreationTime = address.CreationTime,
                                UpdateTime = address.UpdateTime,
                                DeletionTime = address.DeletionTime
                            },
                            Password = new ReturnPasswordDto
                            {
                                PasswordId = password.PasswordId,
                                PasswordHash = password.PasswordHash,
                                PasswordSalt = password.PasswordSalt,
                                AttemptCount = password.AttemptCount,
                                UpdateTime = password.UpdateTime,
                                CreationTime = password.CreationTime
                            }
                        }).FirstOrDefaultAsync(cancellationToken);
        }

        public new async Task<List<ReturnCustomerDto>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                select new ReturnCustomerDto
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Active = customer.Active,
                    CreationTime = customer.CreationTime,
                    UpdateTime = customer.UpdateTime,
                    DeletionTime = customer.DeletionTime,
                    Address = new ReturnAddressDto
                    {
                        AddressId = address.AddressId,
                        AddressLine = address.AddressLine,
                        City = address.City,
                        Country = address.Country,
                        ZipCode = address.ZipCode,
                        Complement = address.Complement,
                        CreationTime = address.CreationTime,
                        UpdateTime = address.UpdateTime,
                        DeletionTime = address.DeletionTime
                    }
                }).ToListAsync(cancellationToken);
        }
    }
}