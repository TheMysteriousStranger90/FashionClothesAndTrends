using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FluentAssertions;
using Moq;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class FavoriteItemServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly FavoriteItemService _sut;

    public FavoriteItemServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _sut = new FavoriteItemService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddFavoriteAsync_WhenNotAlreadyFavorite_ShouldAddAndSave()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var userId = "user-123";

        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository.IsFavoriteAsync(clothingItemId, userId))
            .ReturnsAsync(false);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.AddFavoriteAsync(clothingItemId, userId);

        // Assert
        _unitOfWorkMock.Verify(
            u => u.FavoriteItemRepository.Add(It.Is<FavoriteItem>(f =>
                f.ClothingItemId == clothingItemId && f.UserId == userId)), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddFavoriteAsync_WhenAlreadyFavorite_ShouldThrowConflictException()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var userId = "user-123";

        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository.IsFavoriteAsync(clothingItemId, userId))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _sut.AddFavoriteAsync(clothingItemId, userId);

        // Assert
        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already in the favorites*");
    }

    [Fact]
    public async Task RemoveFavoriteAsync_WhenFavoriteExists_ShouldDeleteAndSave()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var userId = "user-123";
        var favorite = new FavoriteItem { ClothingItemId = clothingItemId, UserId = userId };

        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository
            .GetByClothingItemIdAndUserIdAsync(clothingItemId, userId)).ReturnsAsync(favorite);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.RemoveFavoriteAsync(clothingItemId, userId);

        // Assert
        _unitOfWorkMock.Verify(u => u.FavoriteItemRepository.Delete(favorite), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveFavoriteAsync_WhenFavoriteNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository
                .GetByClothingItemIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync((FavoriteItem?)null);

        // Act
        Func<Task> act = async () => await _sut.RemoveFavoriteAsync(Guid.NewGuid(), "user-1");

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Favorite item not found*");
    }

    [Fact]
    public async Task GetFavoritesByUserIdAsync_WhenFavoritesExist_ShouldReturnMappedDtos()
    {
        // Arrange
        var userId = "user-123";
        var favorites = new List<FavoriteItem>
        {
            new() { UserId = userId, ClothingItemId = Guid.NewGuid() },
            new() { UserId = userId, ClothingItemId = Guid.NewGuid() }
        };
        var dtos = favorites.Select(f => new FavoriteItemDto { UserDtoId = f.UserId }).ToList();

        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository.GetFavoritesByUserIdAsync(userId))
            .ReturnsAsync(favorites);
        _mapperMock.Setup(m => m.Map<IEnumerable<FavoriteItemDto>>(favorites)).Returns(dtos);

        // Act
        var result = await _sut.GetFavoritesByUserIdAsync(userId);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task IsFavoriteAsync_ShouldDelegateToRepository()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var userId = "user-123";
        _unitOfWorkMock.Setup(u => u.FavoriteItemRepository.IsFavoriteAsync(clothingItemId, userId))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.IsFavoriteAsync(clothingItemId, userId);

        // Assert
        result.Should().BeTrue();
    }
}
