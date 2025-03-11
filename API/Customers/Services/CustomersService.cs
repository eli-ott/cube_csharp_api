using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Extensions;
using MonApi.API.CartLines.Repositories;
using MonApi.API.Carts.DTOs;
using MonApi.API.Carts.Extensions;
using MonApi.API.Carts.Repositories;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Extensions;
using MonApi.API.Customers.Filters;
using MonApi.API.Customers.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Passwords.Repositories;
using MonApi.API.Products.Repositories;
using MonApi.API.Reviews.Extensions;
using MonApi.API.Reviews.Models;
using MonApi.API.Reviews.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;
using MonApi.Shared.Utils;

namespace MonApi.API.Customers.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartLineRepository _cartLineRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IEmailSender _emailSender;

        public CustomersService(ICustomersRepository customersRepository, IPasswordRepository passwordRepository,
            IAddressRepository addressRepository, IReviewRepository reviewRepository, IEmailSender emailSender,
            ICartRepository cartRepository, ICartLineRepository cartLineRepository,
            IProductsRepository productsRepository)
        {
            _customersRepository = customersRepository;
            _passwordRepository = passwordRepository;
            _addressRepository = addressRepository;
            _reviewRepository = reviewRepository;
            _cartRepository = cartRepository;
            _cartLineRepository = cartLineRepository;
            _productsRepository = productsRepository;
            _emailSender = emailSender;
        }

        public async Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto)
        {
            if (await _customersRepository.AnyAsync(u => u.Email == registerDto.Email))
                throw new ArgumentException("Email already exists");

            var address = registerDto.Address.MapToAddressModel();
            var addedAddress = await _addressRepository.AddAsync(address);

            var password = PasswordExtensions.MapToPasswordModel(registerDto);

            // Hachage du mot de passe
            var hashedPassword = PasswordUtils.HashPassword(registerDto.Password, out var salt);
            password.PasswordHash = hashedPassword;
            password.PasswordSalt = Convert.ToBase64String(salt);

            var baseUrl = Environment.GetEnvironmentVariable("URL_FRONT")
                          ?? throw new KeyNotFoundException("L'url du front n'est pas disponible");

            var guid = Guid.NewGuid();
            var completeUrl = $"{baseUrl}/confirm-registration/{registerDto.Email}/{guid}";

            var emailContent =
                $"Bienvenue sur NegoSud, pour confirmer votre compte veuillez utiliser sur le lien suivant : {completeUrl}";
            var emailSubject = "Inscription a NegoSud";

            await _emailSender.SendEmailAsync(registerDto.Email, emailSubject, emailContent);

            var addedPassword = await _passwordRepository.AddAsync(password);

            var customer = registerDto.MapToCustomerModel(addedPassword, addedAddress, guid);

            customer.Email = customer.Email.ToLower();

            var newCustomer = await _customersRepository.AddAsync(customer);
            var newCustomerDetails = await _customersRepository.FindAsync(newCustomer.CustomerId);

            // We create the user's cart
            var newCart = CartExtensions.MapToModel(newCustomerDetails!.CustomerId);
            await _cartRepository.AddAsync(newCart);

            return newCustomerDetails;
        }

        public async Task<string> LogCustomer(LoginDTO loginDto)
        {
            var foundCustomer = await _customersRepository.FindByEmailAsync(loginDto.Email);

            if (foundCustomer == null)
                throw new KeyNotFoundException("Utilisateur introuvable");
            if (foundCustomer.DeletionTime != null)
                throw new BadHttpRequestException("L'utilisateur est supprimé");
            if (!foundCustomer.Active)
                throw new BadHttpRequestException("L'utilisateur n'est pas actif");

            var passwordValid = PasswordUtils.VerifyPassword(
                loginDto.Password,
                foundCustomer.Password!.PasswordHash,
                Convert.FromBase64String(foundCustomer.Password.PasswordSalt)
            );

            // Si le mot de passe est mauvais on incrémente le nombre d'essais
            if (!passwordValid)
            {
                // Si il y a trop d'essais on retourne une erreur
                if (foundCustomer.Password!.AttemptCount >= 3)
                    throw new AuthenticationException(
                        "Nombre d'essais trop important, veuillez réinitialiser votre mot de passe");

                // On incrémente le nombre d'essais
                foundCustomer.Password.AttemptCount++;

                var passwordModelInvalid = foundCustomer.Password.MapToPasswordModel();
                await _passwordRepository.UpdateAsync(passwordModelInvalid);

                throw new AuthenticationException("Mot de passe incorrect");
            }

            // Mise à jour du nombre d'essais si l'utilisateur se connecte correctement
            foundCustomer.Password.AttemptCount = 0;

            var passwordModel = foundCustomer.Password.MapToPasswordModel();
            await _passwordRepository.UpdateAsync(passwordModel);

            var userCart = await _cartRepository.GetCart(foundCustomer.CustomerId)
                           ?? throw new NullReferenceException("Can't find the user cart");

            // Faire une liste de Claims 
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Role, "Customer"),
                new("CustomerID", foundCustomer.CustomerId.ToString()),
                new("Email", foundCustomer.Email),
                new("FirstName", foundCustomer.FirstName),
                new("CartId", userCart.CartId.ToString())
            };

            // Signer le token de connexion JWT
            var key = Environment.GetEnvironmentVariable("JWT_SECRET")
                      ?? throw new KeyNotFoundException("JWT_SECRET");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            // On créer un objet de token à partir de la clé de sécurité et l'on y ajoute une expiration, une audience et un issuer de sorte à pouvoir cibler nos clients d'API et éviter les tokens qui trainent trop longtemps dans la nature
            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,
                issuer: "Issuer",
                audience: "Audience",
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(12));

            // Générer le JWT à partir de l'objet JWT 
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var customer = await _customersRepository.FindByEmailAsync(resetPasswordDto.Email)
                           ?? throw new NullReferenceException("L'utilisateur n'existe pas");
            if (customer.DeletionTime != null) throw new SoftDeletedException("This customer has been deleted.");

            var password = await _passwordRepository.FindAsync(customer.Password!.PasswordId)
                           ?? throw new NullReferenceException("Le mot de passe à réinitialiser est introuvable");
            if (password.DeletionTime != null) throw new SoftDeletedException("This password has been deleted.");

            var passwordHash = PasswordUtils.HashPassword(resetPasswordDto.Password, out var salt);

            password.PasswordHash = passwordHash;
            password.PasswordSalt = Convert.ToBase64String(salt);
            password.AttemptCount = 0;
            password.ResetDate = DateTime.UtcNow;

            await _passwordRepository.UpdateAsync(password);
        }

        public async Task ConfirmRegistration(string email, string guid)
        {
            var customer = await _customersRepository.FindByEmailAsync(email)
                           ?? throw new NullReferenceException("Le clent n'éxiste pas");
            if (customer.DeletionTime != null) throw new SoftDeletedException("This customer has been deleted.");

            if (customer.ValidationId != guid)
                throw new BadHttpRequestException("Le guid n'est pas valide");

            customer.Active = true;
            var customerModel = customer.MapToCustomerModel();

            await _customersRepository.UpdateAsync(customerModel);
        }

        public async Task<PagedResult<ReturnCustomerDto>> GetAllCustomers(CustomerQueryParameters queryParameters)
        {
            return await _customersRepository.ListAsync(queryParameters);
        }

        public async Task<ReturnCustomerDto> GetCustomerById(int customerId)
        {
            var customer = await _customersRepository.FindAsync(customerId)
                           ?? throw new NullReferenceException("Le client n'éxiste pas");

            return customer;
        }

        public async Task<ReturnCustomerDto> UpdateCustomer(int customerId, UpdateCustomerDto updateCustomerDto)
        {
            var foundCustomer = await _customersRepository.FindWithPasswordAsync(customerId)
                                ?? throw new NullReferenceException("Le client n'éxiste pas");
            if (foundCustomer.DeletionTime != null) throw new SoftDeletedException("This customer has been deleted.");

            foundCustomer.LastName = updateCustomerDto.LastName;
            foundCustomer.FirstName = updateCustomerDto.FirstName;
            foundCustomer.Phone = updateCustomerDto.Phone;

            var foundAddress = await _addressRepository.FindAsync(foundCustomer.Address.AddressId)
                               ?? throw new NullReferenceException("L'adresse relié à l'utilisateur est introuvable");
            if (foundAddress.DeletionTime != null)
                throw new SoftDeletedException("This address has been deleted.");

            if (foundAddress.AddressId != updateCustomerDto.Address.AddressId)
                throw new BadHttpRequestException("L'adresse ne correspond pas au client");

            // Updating the address values
            foundAddress.Country = updateCustomerDto.Address.Country;
            foundAddress.AddressLine = updateCustomerDto.Address.AddressLine;
            foundAddress.City = updateCustomerDto.Address.City;
            foundAddress.ZipCode = updateCustomerDto.Address.ZipCode;
            foundCustomer.Address.Complement = updateCustomerDto.Address.Complement;

            // Updating the address
            await _addressRepository.UpdateAsync(foundAddress);

            // Checking if the user email changed
            // If it changed we set its active status to 0 and resend an email to confirm the new email it's his
            if (foundCustomer.Email != updateCustomerDto.Email)
            {
                foundCustomer.Active = false;
                foundCustomer.Email = updateCustomerDto.Email;

                var baseUrl = Environment.GetEnvironmentVariable("URL_FRONT")
                              ?? throw new KeyNotFoundException("L'url du front n'est pas disponible");

                var guid = Guid.NewGuid();
                var completeUrl = $"{baseUrl}/confirm-registration/{foundCustomer.Email}/{guid}";

                var emailContent =
                    $"Pour confirmer votre nouvelle adresse veuillez utiliser le lien suivant : {completeUrl}";
                var emailSubject = "Changement d'email sur NegoSud";

                await _emailSender.SendEmailAsync(foundCustomer.Email, emailSubject,
                    emailContent);

                foundCustomer.ValidationId = guid.ToString();
            }

            await _customersRepository.UpdateAsync(foundCustomer.MapToCustomerModel());

            // Removing useless properties for return
            foundCustomer.Password = null;
            foundCustomer.ValidationId = null;
            return foundCustomer;
        }

        public async Task SoftDeleteCustomer(int customerId)
        {
            var foundCustomer = await _customersRepository.FindWithPasswordAsync(customerId)
                                ?? throw new NullReferenceException("L'utilisateur est introuvable");
            if (foundCustomer.DeletionTime != null)
                throw new SoftDeletedException("This customer has been deleted already.");

            var foundAddress = await _addressRepository.FindAsync(foundCustomer.Address.AddressId)
                               ?? throw new NullReferenceException("L'adresse est introuvable");
            if (foundAddress.DeletionTime != null)
                throw new SoftDeletedException("This address has been deleted already.");

            var foundPassword = await _passwordRepository.FindAsync(foundCustomer.Password!.PasswordId)
                                ?? throw new NullReferenceException(
                                    "Le mot de passe associé à l'utilisateur est introuvable");
            if (foundPassword.DeletionTime != null)
                throw new SoftDeletedException("This password has been deleted already.");

            var reviews = foundCustomer.Reviews;
            if (reviews != null && reviews.Count > 0)
            {
                var mappedReviews = reviews.Select(x => x.MapReviewToModel()).ToList();
                await _reviewRepository.RemoveRangeAsync(mappedReviews);
            }

            var foundCart = await _cartRepository.FirstOrDefaultAsync(cart => cart.CustomerId == customerId)
                            ?? throw new NullReferenceException("Can't find user's cart");

            var foundCartLines = await _cartLineRepository.ListAsync(x => x.CartId == foundCart.CartId);
            await _cartLineRepository.RemoveRangeAsync(foundCartLines);

            await _cartRepository.DeleteAsync(foundCart);

            // Update the password deletion time
            foundPassword.DeletionTime = DateTime.UtcNow;
            await _passwordRepository.UpdateAsync(foundPassword);

            // Update the address deletion time
            foundAddress.DeletionTime = DateTime.UtcNow;
            await _addressRepository.UpdateAsync(foundAddress);

            foundCustomer.DeletionTime = DateTime.UtcNow;
            await _customersRepository.UpdateAsync(foundCustomer.MapToCustomerModel());
        }

        public async Task<ReturnCartDto> GetCart(int customerId)
        {
            var foundUser = await _customersRepository.FindAsync(customerId)
                            ?? throw new NullReferenceException("Can't find the user");
            if (foundUser.DeletionTime != null)
                throw new SoftDeletedException("User has been deleted");

            var foundCart = await _cartRepository.GetCart(customerId)
                            ?? throw new NullReferenceException("Can't find the user cart");

            return foundCart;
        }

        public async Task AddToCart(int customerId, CreateCartLineDto createCartLineDto)
        {
            var foundUser = await _customersRepository.FindAsync(customerId)
                            ?? throw new NullReferenceException("Can't find the user");
            if (foundUser.DeletionTime != null)
                throw new SoftDeletedException("User has been deleted");

            var foundProduct = await _productsRepository.FindProduct(createCartLineDto.ProductId)
                               ?? throw new NullReferenceException("Can't find the product");
            if (foundProduct.DeletionTime != null)
                throw new SoftDeletedException("Product has been deleted");

            var foundCart = await _cartRepository.GetCart(customerId)
                            ?? throw new NullReferenceException("Can't find the user cart");

            foreach (var line in foundCart.CartLines)
            {
                if (line.Product.ProductId == createCartLineDto.ProductId)
                    throw new BadHttpRequestException("Product is already in the cart");
            }

            var mappedCartLine = createCartLineDto.MapToModel(foundCart.CartId);

            await _cartLineRepository.AddAsync(mappedCartLine);
        }
    }
}