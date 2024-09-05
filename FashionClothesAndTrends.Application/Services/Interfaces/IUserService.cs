using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using Stripe;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IUserService
{
    Task<AddressDto> GetUserAddress(string userName);
    Task<AddressDto> UpdateUserAddress(AddressDto address, string userName);
    Task<UserDto> GetUserByUsernameAsync(string userName);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
    Task<IReadOnlyList<UserDto>> SearchUsersByNameAsync(string name);
    Task<UserPhotoDto> AddPhotoByUser(ImageUploadResult result, string userName);
    Task SetMainUserPhotoByUser(Guid userPhotoId, string userName);
    Task DeleteUserPhotoByUser(Guid userPhotoId, string userName);
}