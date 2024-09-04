using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Mapping;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using FashionClothesAndTrends.Infrastructure.Repositories;
using FashionClothesAndTrends.Infrastructure.UoW;
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
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(options);
        });
        
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        services.AddScoped<IBasketService, BasketService>();
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddAutoMapper(typeof(AutoMapperProfile));
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderHistoryService, OrderHistoryService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<ILikeDislikeService, LikeDislikeService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IFavoriteItemService, FavoriteItemService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IClothingItemService, ClothingItemService>();
        services.AddScoped<ICouponService, CouponService>();
        
        return services;
    }
}