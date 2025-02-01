using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MonApi.API.Auth.Services;
using MonApi.API.Customers.Repositories;
using MonApi.API.Customers.Services;
using MonApi.API.Passwords.Repositories;
using MonApi.API.Passwords.Services;
using MonApi.Shared.Data;
using System.Text;

namespace MonApi.Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {

        public static void InjectDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.AddServices();
            builder.AddRepositories();
            builder.AddJWT();
            builder.AddSwagger();
            builder.AddEFCoreConfiguration();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
        }

        public static void AddJWT(this WebApplicationBuilder builder)
        {
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT secret 'JWT_SECRET' not found.");

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
                ?? throw new InvalidOperationException("Connection string 'DATABASE_CONNECTION_STRING' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
