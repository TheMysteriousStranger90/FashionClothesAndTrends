using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
    IClothingItemRepository ClothingItemRepository { get; }
    ICommentRepository CommentRepository { get; }
    IFavoriteItemRepository FavoriteItemRepository { get; }
    ILikeDislikeRepository LikeDislikeRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IRatingRepository RatingRepository { get; }
    IWishlistRepository WishlistRepository { get; }

    IUserRepository UserRepository { get; }
    UserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }

    Task<int> SaveAsync();
}