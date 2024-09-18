using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class LikesControllerTests
{
    private readonly Mock<ILikeDislikeService> _likeDislikeServiceMock;
    private readonly LikesController _controller;

    public LikesControllerTests()
    {
        _likeDislikeServiceMock = new Mock<ILikeDislikeService>();
        _controller = new LikesController(_likeDislikeServiceMock.Object);
    }

    [Fact]
    public async Task AddLikeDislike_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var likeDislikeDto = new LikeDislikeDto { IsLike = true, CommentId = Guid.NewGuid(), UserId = "test_user" };

        // Act
        var result = await _controller.AddLikeDislike(likeDislikeDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task AddLikeDislike_ReturnsBadRequest_WhenArgumentNullExceptionThrown()
    {
        // Arrange
        var likeDislikeDto = new LikeDislikeDto { IsLike = true, CommentId = Guid.NewGuid(), UserId = "test_user" };
        _likeDislikeServiceMock.Setup(service => service.AddLikeDislikeAsync(likeDislikeDto))
            .ThrowsAsync(new ArgumentNullException(nameof(likeDislikeDto)));

        // Act
        var result = await _controller.AddLikeDislike(likeDislikeDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Contains(nameof(likeDislikeDto), apiResponse.Message);
    }

    [Fact]
    public async Task AddLikeDislike_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var likeDislikeDto = new LikeDislikeDto { IsLike = true, CommentId = Guid.NewGuid(), UserId = "test_user" };
        _likeDislikeServiceMock.Setup(service => service.AddLikeDislikeAsync(likeDislikeDto))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.AddLikeDislike(likeDislikeDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task RemoveLikeDislike_ReturnsNoContentResult_WhenSuccessful()
    {
        // Arrange
        var likeDislikeId = Guid.NewGuid();

        // Act
        var result = await _controller.RemoveLikeDislike(likeDislikeId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveLikeDislike_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var likeDislikeId = Guid.NewGuid();
        _likeDislikeServiceMock.Setup(service => service.RemoveLikeDislikeAsync(likeDislikeId))
            .ThrowsAsync(new NotFoundException("Like/Dislike not found."));

        // Act
        var result = await _controller.RemoveLikeDislike(likeDislikeId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("Like/Dislike not found.", apiResponse.Message);
    }

    [Fact]
    public async Task RemoveLikeDislike_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var likeDislikeId = Guid.NewGuid();
        _likeDislikeServiceMock.Setup(service => service.RemoveLikeDislikeAsync(likeDislikeId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.RemoveLikeDislike(likeDislikeId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetLikesDislikesByUserId_ReturnsOkResult_WithLikesDislikes()
    {
        // Arrange
        var userId = "test_user";
        var likesDislikes = new List<LikeDislikeDto>
        {
            new LikeDislikeDto { IsLike = true, CommentId = Guid.NewGuid(), UserId = userId },
            new LikeDislikeDto { IsLike = false, CommentId = Guid.NewGuid(), UserId = userId }
        };
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByUserIdAsync(userId))
            .ReturnsAsync(likesDislikes);

        // Act
        var result = await _controller.GetLikesDislikesByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedLikesDislikes = Assert.IsType<List<LikeDislikeDto>>(okResult.Value);
        Assert.Equal(likesDislikes.Count, returnedLikesDislikes.Count);
    }

    [Fact]
    public async Task GetLikesDislikesByUserId_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var userId = "test_user";
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByUserIdAsync(userId))
            .ThrowsAsync(new NotFoundException("No likes/dislikes found for this user."));

        // Act
        var result = await _controller.GetLikesDislikesByUserId(userId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("No likes/dislikes found for this user.", apiResponse.Message);
    }

    [Fact]
    public async Task GetLikesDislikesByUserId_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var userId = "test_user";
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByUserIdAsync(userId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetLikesDislikesByUserId(userId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetLikesDislikesByCommentId_ReturnsOkResult_WithLikesDislikes()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var likesDislikes = new List<LikeDislikeDto>
        {
            new LikeDislikeDto { IsLike = true, CommentId = commentId, UserId = "test_user" },
            new LikeDislikeDto { IsLike = false, CommentId = commentId, UserId = "test_user" }
        };
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByCommentIdAsync(commentId))
            .ReturnsAsync(likesDislikes);

        // Act
        var result = await _controller.GetLikesDislikesByCommentId(commentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedLikesDislikes = Assert.IsType<List<LikeDislikeDto>>(okResult.Value);
        Assert.Equal(likesDislikes.Count, returnedLikesDislikes.Count);
    }

    [Fact]
    public async Task GetLikesDislikesByCommentId_ReturnsNotFound_WhenNotFoundExceptionThrown()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByCommentIdAsync(commentId))
            .ThrowsAsync(new NotFoundException("No likes/dislikes found for this comment."));

        // Act
        var result = await _controller.GetLikesDislikesByCommentId(commentId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal("No likes/dislikes found for this comment.", apiResponse.Message);
    }

    [Fact]
    public async Task GetLikesDislikesByCommentId_ReturnsInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _likeDislikeServiceMock.Setup(service => service.GetLikesDislikesByCommentIdAsync(commentId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetLikesDislikesByCommentId(commentId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }
}