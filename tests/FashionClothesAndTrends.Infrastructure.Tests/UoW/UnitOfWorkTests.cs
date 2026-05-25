using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Infrastructure.Context;
using FashionClothesAndTrends.Infrastructure.UoW;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FashionClothesAndTrends.Infrastructure.Tests.UoW;

public class UnitOfWorkTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly UnitOfWork _sut;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        var userStore = new Mock<IUserStore<User>>();
        var userManager = new Mock<UserManager<User>>(
            userStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        var roleStore = new Mock<IRoleStore<AppRole>>();
        var roleManager = new Mock<RoleManager<AppRole>>(
            roleStore.Object, null!, null!, null!, null!);

        var signInManager = new Mock<SignInManager<User>>(
            userManager.Object,
            Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            null!, null!, null!, null!);

        _sut = new UnitOfWork(_context, userManager.Object, signInManager.Object, roleManager.Object);
    }

    [Fact]
    public void GenericRepository_ShouldReturnRepositoryForType()
    {
        // Act
        var repo = _sut.GenericRepository<ClothingItem>();

        // Assert
        repo.Should().NotBeNull();
    }

    [Fact]
    public void GenericRepository_CalledTwiceForSameType_ShouldReturnSameInstance()
    {
        // Act
        var repo1 = _sut.GenericRepository<ClothingItem>();
        var repo2 = _sut.GenericRepository<ClothingItem>();

        // Assert - should be cached/same instance
        repo1.Should().BeSameAs(repo2);
    }

    [Fact]
    public void GenericRepository_ForDifferentTypes_ShouldReturnDifferentInstances()
    {
        // Act
        var itemRepo = _sut.GenericRepository<ClothingItem>();
        var brandRepo = _sut.GenericRepository<ClothingBrand>();

        // Assert
        itemRepo.Should().NotBeSameAs(brandRepo);
    }

    [Fact]
    public void ClothingItemRepository_ShouldNotBeNull()
    {
        // Act & Assert
        _sut.ClothingItemRepository.Should().NotBeNull();
    }

    [Fact]
    public void ClothingItemRepository_AccessedTwice_ShouldReturnSameInstance()
    {
        // Act
        var r1 = _sut.ClothingItemRepository;
        var r2 = _sut.ClothingItemRepository;

        // Assert
        r1.Should().BeSameAs(r2);
    }

    [Fact]
    public void CommentRepository_ShouldNotBeNull()
    {
        _sut.CommentRepository.Should().NotBeNull();
    }

    [Fact]
    public void FavoriteItemRepository_ShouldNotBeNull()
    {
        _sut.FavoriteItemRepository.Should().NotBeNull();
    }

    [Fact]
    public void LikeDislikeRepository_ShouldNotBeNull()
    {
        _sut.LikeDislikeRepository.Should().NotBeNull();
    }

    [Fact]
    public void NotificationRepository_ShouldNotBeNull()
    {
        _sut.NotificationRepository.Should().NotBeNull();
    }

    [Fact]
    public void RatingRepository_ShouldNotBeNull()
    {
        _sut.RatingRepository.Should().NotBeNull();
    }

    [Fact]
    public void WishlistRepository_ShouldNotBeNull()
    {
        _sut.WishlistRepository.Should().NotBeNull();
    }

    [Fact]
    public void PhotoRepository_ShouldNotBeNull()
    {
        _sut.PhotoRepository.Should().NotBeNull();
    }

    [Fact]
    public void OrderHistoryRepository_ShouldNotBeNull()
    {
        _sut.OrderHistoryRepository.Should().NotBeNull();
    }

    [Fact]
    public void CouponRepository_ShouldNotBeNull()
    {
        _sut.CouponRepository.Should().NotBeNull();
    }

    [Fact]
    public void UserRepository_ShouldNotBeNull()
    {
        _sut.UserRepository.Should().NotBeNull();
    }

    [Fact]
    public async Task SaveAsync_WithNewEntity_ShouldPersistToContext()
    {
        // Arrange
        var brand = new ClothingBrand
        {
            Id = Guid.NewGuid(),
            Name = "Save Test Brand",
            Description = "Test"
        };
        _context.ClothingBrands.Add(brand);

        // Act
        var result = await _sut.SaveAsync();

        // Assert
        result.Should().Be(1);
        _context.ClothingBrands.Should().Contain(b => b.Name == "Save Test Brand");
    }

    [Fact]
    public async Task SaveAsync_WithMultipleEntities_ShouldReturnCorrectCount()
    {
        // Arrange
        _context.ClothingBrands.AddRange(
            new ClothingBrand { Id = Guid.NewGuid(), Name = "Brand X" },
            new ClothingBrand { Id = Guid.NewGuid(), Name = "Brand Y" }
        );

        // Act
        var result = await _sut.SaveAsync();

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    public void UserManager_ShouldBeAvailable()
    {
        _sut.UserManager.Should().NotBeNull();
    }

    [Fact]
    public void SignInManager_ShouldBeAvailable()
    {
        _sut.SignInManager.Should().NotBeNull();
    }

    [Fact]
    public void RoleManager_ShouldBeAvailable()
    {
        _sut.RoleManager.Should().NotBeNull();
    }

    public void Dispose()
    {
        _sut.Dispose();
        _context.Dispose();
    }
}
