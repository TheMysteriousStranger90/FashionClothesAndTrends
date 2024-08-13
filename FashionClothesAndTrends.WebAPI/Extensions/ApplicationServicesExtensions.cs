using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace FashionClothesAndTrends.WebAPI.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        
        services.AddSingleton<IConnectionMultiplexer>(c => 
        {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });
        
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        services.AddScoped<IBasketService, BasketService>();
        
        return services;
    }
}