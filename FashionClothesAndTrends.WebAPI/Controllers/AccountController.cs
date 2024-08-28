using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        try
        {
            var user = await _authService.RegisterAsync(registerDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        try
        {
            var user = await _authService.LoginAsync(loginDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
    
    [HttpPost("confirm-email")]
    public async Task<ActionResult<bool>> ConfirmEmail(string userName, string token)
    {
        try
        {
            var result = await _authService.ConfirmEmailAsync(userName, token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<bool>> ResetPassword(string userName, string token, string newPassword)
    {
        try
        {
            var result = await _authService.ResetPasswordAsync(userName, token, newPassword);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<bool>> ChangePassword(string userName, string currentPassword, string newPassword)
    {
        try
        {
            var result = await _authService.ChangePasswordAsync(userName, currentPassword, newPassword);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("check-email-exists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        try
        {
            var exists = await _authService.CheckEmailExistsAsync(email);
            return Ok(exists);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("check-username-exists")]
    public async Task<ActionResult<bool>> CheckUserNameExistsAsync([FromQuery] string userName)
    {
        try
        {
            var exists = await _authService.CheckUserNameExistsAsync(userName);
            return Ok(exists);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        return await _authService.FindByEmailFromClaims(User);
    }
}