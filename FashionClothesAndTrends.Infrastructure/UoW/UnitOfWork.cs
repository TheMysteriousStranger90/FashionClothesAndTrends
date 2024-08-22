using System.Collections;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using FashionClothesAndTrends.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private Hashtable _repositories;

    private IClothingItemRepository _clothingItemRepository;
    private ICommentRepository _commentRepository;
    private IFavoriteItemRepository _favoriteItemRepository;
    private ILikeDislikeRepository _likeDislikeRepository;
    private INotificationRepository _notificationRepository;
    private IRatingRepository _ratingRepository;
    private IWishlistRepository _wishlistRepository;
    private IPhotoRepository _photoRepository;
    private IUserRepository _userRepository;

    public UnitOfWork(
        ApplicationDbContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<AppRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (GenericRepository<T>)_repositories[type];
    }

    public IClothingItemRepository ClothingItemRepository =>
        _clothingItemRepository ??= new ClothingItemRepository(_context);

    public ICommentRepository CommentRepository =>
        _commentRepository ??= new CommentRepository(_context);

    public IFavoriteItemRepository FavoriteItemRepository =>
        _favoriteItemRepository ??= new FavoriteItemRepository(_context);

    public ILikeDislikeRepository LikeDislikeRepository =>
        _likeDislikeRepository ??= new LikeDislikeRepository(_context);

    public INotificationRepository NotificationRepository =>
        _notificationRepository ??= new NotificationRepository(_context);

    public IRatingRepository RatingRepository =>
        _ratingRepository ??= new RatingRepository(_context);

    public IWishlistRepository WishlistRepository =>
        _wishlistRepository ??= new WishlistRepository(_context);
    
    public IPhotoRepository PhotoRepository => 
        _photoRepository ??= new PhotoRepository(_context);

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_context);

    public UserManager<User> UserManager => _userManager;

    public SignInManager<User> SignInManager => _signInManager;

    public RoleManager<AppRole> RoleManager => _roleManager;

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}