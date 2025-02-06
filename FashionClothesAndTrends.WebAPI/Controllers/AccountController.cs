using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class AccountController : BaseApiController
{
    private readonly IAuthService _authService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IAuthService authService,
        ILogger<AccountController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <response code="200">Returns the newly created user</response>
    /// <response code="400">If the registration data is invalid</response>
    /// <response code="429">Too many requests</response>
    [HttpPost("register")]
    [EnableRateLimiting("registration")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            _logger.LogInformation("Attempting to register user: {Email}", registerDto.Email);

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });

            var user = await _authService.RegisterAsync(registerDto);

            _logger.LogInformation("User registered successfully: {Email}", registerDto.Email);

            return Ok(user);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed for registration: {Email}", registerDto.Email);
            return BadRequest(new ErrorResponse { Errors = new[] { ex.Message } });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user: {Email}", registerDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse { Errors = new[] { "An error occurred while registering the user." } });
        }
    }

    /// <summary>
    /// Authenticates a user
    /// </summary>
    [HttpPost("login")]
    [EnableRateLimiting("authentication")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        try
        {
            _logger.LogInformation("Login attempt for user: {Email}", loginDto.Email);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.LoginAsync(loginDto);

            _logger.LogInformation("User logged in successfully: {Email}", loginDto.Email);

            return Ok(user);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Failed login attempt for user: {Email}", loginDto.Email);
            return Unauthorized(new ErrorResponse { Errors = new[] { ex.Message } });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user: {Email}", loginDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError);
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
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        try
        {
            var user = await _authService.FindByEmailFromClaims(User);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving current user");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}