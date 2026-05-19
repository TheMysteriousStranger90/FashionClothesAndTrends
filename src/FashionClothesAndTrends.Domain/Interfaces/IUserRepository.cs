using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUserName(string userName);
    Task<IReadOnlyList<User>> GetAllUsersAsync();
    Task<IReadOnlyList<User>> GetUsersByRoleAsync(AppUserRole role);
    Task<IReadOnlyList<User>> SearchUsersByNameAsync(string name);
}