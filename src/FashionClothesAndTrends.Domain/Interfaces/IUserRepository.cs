using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task<IReadOnlyList<User>> GetAllUsersAsync();
    Task<IReadOnlyList<User>> GetUsersByRoleAsync(AppUserRole role);
    Task<IReadOnlyList<User>> SearchUsersByNameAsync(string name);
}
