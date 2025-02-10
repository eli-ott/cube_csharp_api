using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Services;

namespace MonApi.API.Passwords.Controllers;

[ApiController]
[Authorize]
[Route("passwords")]
public class PasswordController : ControllerBase
{
    private readonly IPasswordService _passwordService;

    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordDto passwordDto, int id)
    {
        await _passwordService.UpdateAsync(passwordDto, id);
        return Ok();
    }
}