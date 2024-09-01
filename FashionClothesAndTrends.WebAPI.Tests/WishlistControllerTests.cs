using System.Security.Claims;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class WishlistControllerTests
{
    private readonly Mock<IWishlistService> _wishlistServiceMock;
    private readonly WishlistController _controller;

    public WishlistControllerTests()
    {
        _wishlistServiceMock = new Mock<IWishlistService>();
        _controller = new WishlistController(_wishlistServiceMock.Object);

        // Set up a mock user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test_user_id")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task GetWishlistsByUserId_ReturnsOkResult_WithWishlists()
    {
        // Arrange
        var userId = "test_user_id";
        var wishlists = new List<WishlistDto>
        {
            new WishlistDto { Id = Guid.NewGuid(), Name = "Wishlist 1" },
            new WishlistDto { Id = Guid.NewGuid(), Name = "Wishlist 2" }
        };
        _wishlistServiceMock.Setup(service => service.GetWishlistsByUserIdAsync(userId))
            .ReturnsAsync(wishlists);

        // Act
        var result = await _controller.GetWishlistsByUserId();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedWishlists = Assert.IsType<List<WishlistDto>>(okResult.Value);
        Assert.Equal(wishlists.Count, returnedWishlists.Count);
    }

    [Fact]
    public async Task GetWishlistsByUserId_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.GetWishlistsByUserIdAsync(userId))
            .ThrowsAsync(new NotFoundException("No wishlists found for this user."));

        // Act
        var result = await _controller.GetWishlistsByUserId();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("No wishlists found for this user.", apiResponse.Message);
    }

    [Fact]
    public async Task GetWishlistsByUserId_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.GetWishlistsByUserIdAsync(userId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetWishlistsByUserId();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetWishlistByName_ReturnsOkResult_WithWishlist()
    {
        // Arrange
        var userId = "test_user_id";
        var wishlist = new WishlistDto { Id = Guid.NewGuid(), Name = "Wishlist 1" };
        _wishlistServiceMock.Setup(service => service.GetWishlistByNameAsync(userId, "Wishlist 1"))
            .ReturnsAsync(wishlist);

        // Act
        var result = await _controller.GetWishlistByName("Wishlist 1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedWishlist = Assert.IsType<WishlistDto>(okResult.Value);
        Assert.Equal(wishlist.Name, returnedWishlist.Name);
    }

    [Fact]
    public async Task GetWishlistByName_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.GetWishlistByNameAsync(userId, "Wishlist 1"))
            .ThrowsAsync(new NotFoundException("Wishlist with name 'Wishlist 1' not found for user 'test_user_id'."));

        // Act
        var result = await _controller.GetWishlistByName("Wishlist 1");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("Wishlist with name 'Wishlist 1' not found for user 'test_user_id'.", apiResponse.Message);
    }

    [Fact]
    public async Task GetWishlistByName_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.GetWishlistByNameAsync(userId, "Wishlist 1"))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetWishlistByName("Wishlist 1");

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task CreateWishlist_ReturnsCreatedAtActionResult_WithWishlist()
    {
        // Arrange
        var userId = "test_user_id";
        var wishlist = new WishlistDto { Id = Guid.NewGuid(), Name = "Wishlist 1" };
        _wishlistServiceMock.Setup(service => service.CreateWishlistAsync(userId, "Wishlist 1"))
            .ReturnsAsync(wishlist);

        // Act
        var result = await _controller.CreateWishlist(userId, "Wishlist 1");

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedWishlist = Assert.IsType<WishlistDto>(createdAtActionResult.Value);
        Assert.Equal(wishlist.Name, returnedWishlist.Name);
    }

    [Fact]
    public async Task CreateWishlist_ReturnsConflict_WhenConflictExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.CreateWishlistAsync(userId, "Wishlist 1"))
            .ThrowsAsync(
                new ConflictException("Wishlist with name 'Wishlist 1' already exists for user 'test_user_id'."));

        // Act
        var result = await _controller.CreateWishlist(userId, "Wishlist 1");

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(conflictResult.Value);
        Assert.Equal(409, apiResponse.StatusCode);
        Assert.Equal("Wishlist with name 'Wishlist 1' already exists for user 'test_user_id'.", apiResponse.Message);
    }

    [Fact]
    public async Task CreateWishlist_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var userId = "test_user_id";
        _wishlistServiceMock.Setup(service => service.CreateWishlistAsync(userId, "Wishlist 1"))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.CreateWishlist(userId, "Wishlist 1");

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task DeleteWishlist_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.DeleteWishlistAsync(wishlistId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteWishlist(wishlistId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteWishlist_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.DeleteWishlistAsync(wishlistId))
            .ThrowsAsync(new NotFoundException("Wishlist with ID 'wishlistId' not found."));

        // Act
        var result = await _controller.DeleteWishlist(wishlistId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("Wishlist with ID 'wishlistId' not found.", apiResponse.Message);
    }

    [Fact]
    public async Task DeleteWishlist_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.DeleteWishlistAsync(wishlistId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteWishlist(wishlistId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task AddItemToWishlist_ReturnsOkResult_WithWishlistItem()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var clothingItemId = Guid.NewGuid();
        var wishlistItem = new WishlistItemDto
            { Id = Guid.NewGuid(), ClothingItemId = clothingItemId, ClothingItemName = "Item 1" };
        _wishlistServiceMock.Setup(service => service.AddItemToWishlistAsync(wishlistId, clothingItemId))
            .ReturnsAsync(wishlistItem);

        // Act
        var result = await _controller.AddItemToWishlist(wishlistId, clothingItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedWishlistItem = Assert.IsType<WishlistItemDto>(okResult.Value);
        Assert.Equal(wishlistItem.ClothingItemName, returnedWishlistItem.ClothingItemName);
    }

    [Fact]
    public async Task AddItemToWishlist_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var clothingItemId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.AddItemToWishlistAsync(wishlistId, clothingItemId))
            .ThrowsAsync(new NotFoundException("Wishlist with ID 'wishlistId' not found."));

        // Act
        var result = await _controller.AddItemToWishlist(wishlistId, clothingItemId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("Wishlist with ID 'wishlistId' not found.", apiResponse.Message);
    }

    [Fact]
    public async Task AddItemToWishlist_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var clothingItemId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.AddItemToWishlistAsync(wishlistId, clothingItemId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.AddItemToWishlist(wishlistId, clothingItemId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task RemoveItemFromWishlist_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        _wishlistServiceMock.Setup(service => service.RemoveItemFromWishlistAsync(wishlistId, itemId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveItemFromWishlist(wishlistId, itemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}