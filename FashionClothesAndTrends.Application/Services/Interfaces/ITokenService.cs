using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}