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

public class CommentsControllerTests
{
    private readonly Mock<ICommentService> _commentServiceMock;
    private readonly CommentsController _controller;

    public CommentsControllerTests()
    {
        _commentServiceMock = new Mock<ICommentService>();
        _controller = new CommentsController(_commentServiceMock.Object);
    }

    [Fact]
    public async Task AddComment_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var commentDto = new CommentDto { Id = Guid.NewGuid(), Text = "Test comment", UserId = "test_user" };

        // Act
        var result = await _controller.AddComment(commentDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task AddComment_ReturnsBadRequest_WhenArgumentNullExceptionThrown()
    {
        // Arrange
        var commentDto = new CommentDto { Id = Guid.NewGuid(), Text = "Test comment", UserId = "test_user" };
        _commentServiceMock.Setup(service => service.AddCommentAsync(commentDto))
            .ThrowsAsync(new ArgumentNullException(nameof(commentDto)));

        // Act
        var result = await _controller.AddComment(commentDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Contains(nameof(commentDto), apiResponse.Message);
    }

    [Fact]
    public async Task AddComment_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var commentDto = new CommentDto { Id = Guid.NewGuid(), Text = "Test comment", UserId = "test_user" };
        _commentServiceMock.Setup(service => service.AddCommentAsync(commentDto))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.AddComment(commentDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }
    
    [Fact]
    public async Task RemoveComment_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var userId = "test-user-id";
        _commentServiceMock.Setup(service => service.RemoveCommentAsync(commentId, userId))
            .ThrowsAsync(new NotFoundException("Comment not found."));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }))
            }
        };

        // Act
        var result = await _controller.RemoveComment(commentId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("Comment not found.", apiResponse.Message);
    }

    [Fact]
    public async Task RemoveComment_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var userId = "test-user-id";
        _commentServiceMock.Setup(service => service.RemoveCommentAsync(commentId, userId))
            .ThrowsAsync(new Exception("Test exception"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }))
            }
        };

        // Act
        var result = await _controller.RemoveComment(commentId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetCommentsForClothingItem_ReturnsOkResult_WithComments()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var comments = new List<CommentDto>
        {
            new CommentDto { Id = Guid.NewGuid(), Text = "Test comment 1", UserId = "test_user" },
            new CommentDto { Id = Guid.NewGuid(), Text = "Test comment 2", UserId = "test_user" }
        };
        _commentServiceMock.Setup(service => service.GetCommentsForClothingItemAsync(clothingItemId))
            .ReturnsAsync(comments);

        // Act
        var result = await _controller.GetCommentsForClothingItem(clothingItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedComments = Assert.IsType<List<CommentDto>>(okResult.Value);
        Assert.Equal(comments.Count, returnedComments.Count);
    }

    [Fact]
    public async Task GetCommentsForClothingItem_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        _commentServiceMock.Setup(service => service.GetCommentsForClothingItemAsync(clothingItemId))
            .ThrowsAsync(new NotFoundException("No comments found for this clothing item."));

        // Act
        var result = await _controller.GetCommentsForClothingItem(clothingItemId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("No comments found for this item.", apiResponse.Message);
    }

    [Fact]
    public async Task GetCommentsForClothingItem_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        _commentServiceMock.Setup(service => service.GetCommentsForClothingItemAsync(clothingItemId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetCommentsForClothingItem(clothingItemId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetCommentsByUserId_ReturnsOkResult_WithComments()
    {
        // Arrange
        var userId = "test_user";
        var comments = new List<CommentDto>
        {
            new CommentDto { Id = Guid.NewGuid(), Text = "Test comment 1", UserId = userId },
            new CommentDto { Id = Guid.NewGuid(), Text = "Test comment 2", UserId = userId }
        };
        _commentServiceMock.Setup(service => service.GetCommentsByUserIdAsync(userId))
            .ReturnsAsync(comments);

        // Act
        var result = await _controller.GetCommentsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedComments = Assert.IsType<List<CommentDto>>(okResult.Value);
        Assert.Equal(comments.Count, returnedComments.Count);
    }

    [Fact]
    public async Task GetCommentsByUserId_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var userId = "test_user";
        _commentServiceMock.Setup(service => service.GetCommentsByUserIdAsync(userId))
            .ThrowsAsync(new NotFoundException("No comments found for this user."));

        // Act
        var result = await _controller.GetCommentsByUserId(userId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("No comments found for this user.", apiResponse.Message);
    }

    [Fact]
    public async Task GetCommentsByUserId_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var userId = "test_user";
        _commentServiceMock.Setup(service => service.GetCommentsByUserIdAsync(userId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetCommentsByUserId(userId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }
}