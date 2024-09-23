using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Application.UoW;

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
    IPhotoRepository PhotoRepository { get; }
    IOrderHistoryRepository OrderHistoryRepository { get; }
    IUserRepository UserRepository { get; }
    UserManager<User> UserManager { get; }
    SignInManager<User> SignInManager { get; }
    RoleManager<AppRole> RoleManager { get; }

    Task<int> SaveAsync();
}