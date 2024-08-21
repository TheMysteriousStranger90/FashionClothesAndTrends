using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IAuthService
{
    Task<UserDto> FindByEmailFromClaims(object ob);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<bool> CheckUserNameExistsAsync(string userName);
    Task<bool> CheckEmailExistsAsync(string email);
    Task<bool> ConfirmEmailAsync(string userName, string token);
    Task<string> GenerateEmailConfirmationTokenAsync(string userName);
    Task<bool> ResetPasswordAsync(string userName, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string userName);
    Task<bool> ChangePasswordAsync(string userName, string currentPassword, string newPassword);
}