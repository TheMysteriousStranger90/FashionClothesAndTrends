using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class RatingControllerTests
    {
        private readonly Mock<IRatingService> _ratingServiceMock;
        private readonly RatingController _controller;

        public RatingControllerTests()
        {
            _ratingServiceMock = new Mock<IRatingService>();
            _controller = new RatingController(_ratingServiceMock.Object);
        }

        [Fact]
        public async Task AddRating_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var ratingDto = new RatingDto { ClothingItemId = Guid.NewGuid(), Score = 5, UserId = "test_user", Username = "test_user123"};

            // Act
            var result = await _controller.AddRating(ratingDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddRating_ReturnsBadRequest_WhenArgumentNullExceptionThrown()
        {
            // Arrange
            var ratingDto = new RatingDto { ClothingItemId = Guid.NewGuid(), Score = 5, UserId = "test_user" };
            _ratingServiceMock.Setup(service => service.AddRatingAsync(ratingDto))
                .ThrowsAsync(new ArgumentNullException(nameof(ratingDto)));

            // Act
            var result = await _controller.AddRating(ratingDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal(400, apiResponse.StatusCode);
            Assert.Contains(nameof(ratingDto), apiResponse.Message);
        }

        [Fact]
        public async Task AddRating_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var ratingDto = new RatingDto { ClothingItemId = Guid.NewGuid(), Score = 5, UserId = "test_user" };
            _ratingServiceMock.Setup(service => service.AddRatingAsync(ratingDto))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.AddRating(ratingDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Equal("An error occurred while processing your request", apiResponse.Message);
        }

        [Fact]
        public async Task GetAverageRating_ReturnsOkResult_WithAverageRating()
        {
            // Arrange
            var clothingItemId = Guid.NewGuid();
            var averageRating = 4.5;
            _ratingServiceMock.Setup(service => service.GetAverageRatingAsync(clothingItemId))
                .ReturnsAsync(averageRating);

            // Act
            var result = await _controller.GetAverageRating(clothingItemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAverageRating = Assert.IsType<double>(okResult.Value);
            Assert.Equal(averageRating, returnedAverageRating);
        }

        [Fact]
        public async Task GetAverageRating_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var clothingItemId = Guid.NewGuid();
            _ratingServiceMock.Setup(service => service.GetAverageRatingAsync(clothingItemId))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetAverageRating(clothingItemId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Equal("An error occurred while processing your request", apiResponse.Message);
        }

        [Fact]
        public async Task UpdateRating_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var ratingDto = new RatingDto { ClothingItemId = Guid.NewGuid(), Score = 5, UserId = "test_user" };

            // Act
            var result = await _controller.UpdateRating(ratingDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateRating_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var ratingDto = new RatingDto { ClothingItemId = Guid.NewGuid(), Score = 5, UserId = "test_user" };
            _ratingServiceMock.Setup(service => service.UpdateRatingAsync(ratingDto))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.UpdateRating(ratingDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
            Assert.Equal(500, apiResponse.StatusCode);
            Assert.Equal("An error occurred while processing your request", apiResponse.Message);
        }
    }