using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MonApi.API.Customers.Repositories;
using MonApi.API.Customers.Services;
using MonApi.API.Passwords.Repositories;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Passwords.Services;
using MonApi.API.Families.Repositories;
using MonApi.API.Families.Services;
using MonApi.API.Images.Repositories;
using MonApi.API.Images.Services;
using MonApi.API.Roles.Repositories;
using MonApi.API.Roles.Services;
using MonApi.API.Statuses.Repositories;
using MonApi.API.Statuses.Services;
using MonApi.Shared.Data;
using MonApi.Shared.Utils;
using MonApi.API.Suppliers.Repositories;
using MonApi.API.Suppliers.Services;
using MonApi.API.Products.Repositories;
using MonApi.API.Products.Services;
using MonApi.API.Reviews.Repositories;
using MonApi.API.Reviews.Services;

namespace MonApi.Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void InjectDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.AddServices();
            builder.AddRepositories();
            builder.AddJWT();
            builder.AddSwagger();
            builder.AddEFCoreConfiguration();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<IFamiliesService, FamiliesService>();
            builder.Services.AddScoped<IStatusService, StatusService>();
            builder.Services.AddScoped<ISuppliersService, SuppliersService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IImagesService, ImagesService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IFamiliesRepository, FamiliesRepository>();
            builder.Services.AddScoped<IStatusRepository, StatusRepository>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        }

        public static void AddJWT(this WebApplicationBuilder builder)
        {
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ??
                            throw new InvalidOperationException("JWT secret 'JWT_SECRET' not found.");

            var key = Encoding.ASCII.GetBytes(jwtSecret);

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // Mettre à true en production
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, // Configurer selon les besoins
                        ValidateAudience = false, // Configurer selon les besoins
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddAuthorization();
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddEFCoreConfiguration(this WebApplicationBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                                   ?? throw new InvalidOperationException(
                                       "Connection string 'DATABASE_CONNECTION_STRING' not found.");

            builder.Services.AddDbContext<StockManagementContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}