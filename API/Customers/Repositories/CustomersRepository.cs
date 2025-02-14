using MonApi.Shared.Repositories;
using MonApi.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.API.Customers.Filters;
using MonApi.API.Reviews.DTOs;

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
                    },
                    Reviews = _context.Reviews.Where(r => r.UserId == customer.CustomerId)
                        .Select(r => new ReturnReviewDto
                        {
                            UserId = r.UserId,
                            ProductId = r.ProductId,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            CreationTime = r.CreationTime,
                            UpdateTime = r.UpdateTime,
                            CustomerFirstName = r.User.FirstName,
                            CustomerLastName = r.User.LastName
                        }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindWithPasswordAsync(int id,
            CancellationToken cancellationToken = default)
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
                    },
                    Reviews = _context.Reviews.Where(r => r.UserId == customer.CustomerId)
                        .Select(r => new ReturnReviewDto
                        {
                            UserId = r.UserId,
                            ProductId = r.ProductId,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            CreationTime = r.CreationTime,
                            UpdateTime = r.UpdateTime,
                            CustomerFirstName = r.User.FirstName,
                            CustomerLastName = r.User.LastName
                        }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindByEmailAsync(string email,
            CancellationToken cancellationToken = default)
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

        public async Task<PagedResult<ReturnCustomerDto>> ListAsync(CustomerQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ReturnCustomerDto> query =
                from customer in _context.Customers
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
                };

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryParameters.email))
            {
                query = query.Where(c => c.Email.ToLower() == queryParameters.email.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.first_name))
            {
                query = query.Where(c => c.FirstName.ToLower() == queryParameters.first_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.last_name))
            {
                query = query.Where(c => c.LastName.ToLower() == queryParameters.last_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.phone))
            {
                query = query.Where(c => c.Phone.ToLower() == queryParameters.phone.ToLower());
            }

            if (queryParameters.deleted == "only")
            {
                query = query.Where(f => f.DeletionTime != null);
            }
            else if (queryParameters.deleted == "all")
            {
            }
            else
                // Default to only returning undeleted items
            {
                query = query.Where(f => f.DeletionTime == null);
            }

            if (queryParameters.active == "only")
            {
                query = query.Where(f => f.Active == true);
            }
            else if (queryParameters.active == "all")
            {
            }
            else
                // Default to only returning undeleted items
            {
                query = query.Where(f => f.Active == false);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination: note the multiplication uses PageSize!
            var customers = await query
                .Skip((queryParameters.page - 1) * queryParameters.size)
                .Take(queryParameters.size)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReturnCustomerDto>
            {
                Items = customers,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }
    }
}