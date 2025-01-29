using MonApi.API.Auth.DTOs;
using MonApi.API.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MonApi.API.Auth.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var isCreated = await _authService.RegisterCustomer(registerDTO);
        return Ok(isCreated);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var foundCustomer = await _authService.LogCustomer(loginDTO);

        if (foundCustomer == null)
            return BadRequest(new { Message = "Invalid username or password." });


        // Faire une liste de Claims 
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, "Customer"),
            new Claim("CustomerID", foundCustomer.CustomerId),
            new Claim("Email", foundCustomer.Email),
            new Claim("FirstName", foundCustomer.FirstName)
        };

        // Signer le token de connexion JWT
        var key = Environment.GetEnvironmentVariable("JWT_SECRET");
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        // On créer un objet de token à partir de la clé de sécurité et l'on y ajoute une expiration, une audience et un issuer de sorte à pouvoir cibler nos clients d'API et éviter les tokens qui trainent trop longtemps dans la nature
        JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,
                issuer: "Issuer",
                audience: "Audience",
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(2));

        // Générer le JWT à partir de l'objet JWT 
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Ok(new
        {
            Token = token,
            Message = "Félicitation, vous êtes connecté !"
        });
    }
}
