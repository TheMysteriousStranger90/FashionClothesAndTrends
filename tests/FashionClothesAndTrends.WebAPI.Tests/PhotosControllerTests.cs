using System.Security.Claims;
using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class PhotosControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IClothingItemService> _clothingItemServiceMock;
    private readonly Mock<IPhotoService> _photoServiceMock;
    private readonly PhotosController _controller;

    public PhotosControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _clothingItemServiceMock = new Mock<IClothingItemService>();
        _photoServiceMock = new Mock<IPhotoService>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser")
        }, "mock"));

        var httpContext = new DefaultHttpContext { User = user };
        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        _controller = new PhotosController(_userServiceMock.Object, _clothingItemServiceMock.Object,
            _photoServiceMock.Object)
        {
            ControllerContext = controllerContext
        };
    }

    [Fact]
    public async Task GetUserPhotoById_ReturnsOkResult_WithPhoto()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var photoDto = new UserPhotoDto { Id = photoId, Url = "http://example.com/photo.jpg", IsMain = true };
        _photoServiceMock.Setup(service => service.GetUserPhotoByIdAsync(photoId))
            .ReturnsAsync(photoDto);

        // Act
        var result = await _controller.GetUserPhotoById(photoId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPhoto = Assert.IsType<UserPhotoDto>(okResult.Value);
        Assert.Equal(photoDto.Id, returnedPhoto.Id);
    }

    [Fact]
    public async Task AddUserPhoto_ReturnsOkResult_WithPhoto()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        var userName = "testuser";
        var uploadResult = new ImageUploadResult
            { SecureUrl = new Uri("http://example.com/photo.jpg"), PublicId = "publicId" };
        var photoDto = new UserPhotoDto { Id = Guid.NewGuid(), Url = "http://example.com/photo.jpg", IsMain = true };

        _photoServiceMock.Setup(service => service.AddPhotoAsync(fileMock.Object))
            .ReturnsAsync(uploadResult);
        _userServiceMock.Setup(service => service.AddPhotoByUser(uploadResult, userName))
            .ReturnsAsync(photoDto);

        // Act
        var result = await _controller.AddUserPhoto(fileMock.Object);

        // Assert
        _photoServiceMock.Verify(service => service.AddPhotoAsync(fileMock.Object), Times.Once);
        _userServiceMock.Verify(service => service.AddPhotoByUser(uploadResult, userName), Times.Once);

        var badRequestResult = result.Result as BadRequestObjectResult;
        if (badRequestResult != null)
        {
            var errorMessage = badRequestResult.Value as string;
            Assert.True(false, $"Expected OkObjectResult but got BadRequestObjectResult with message: {errorMessage}");
        }

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPhoto = Assert.IsType<UserPhotoDto>(okResult.Value);
        Assert.Equal(photoDto.Id, returnedPhoto.Id);
    }

    [Fact]
    public async Task SetMainUserPhoto_ReturnsNoContentResult()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userName = "testuser";

        // Act
        var result = await _controller.SetMainUserPhoto(photoId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUserPhoto_ReturnsNoContentResult()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userName = "testuser";

        // Act
        var result = await _controller.DeleteUserPhoto(photoId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetClothingItemPhotoById_ReturnsOkResult_WithPhoto()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var photoDto = new ClothingItemPhotoDto { Id = photoId, Url = "http://example.com/photo.jpg", IsMain = true };
        _photoServiceMock.Setup(service => service.GetClothingItemPhotoByIdAsync(photoId))
            .ReturnsAsync(photoDto);

        // Act
        var result = await _controller.GetClothingItemPhotoById(photoId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPhoto = Assert.IsType<ClothingItemPhotoDto>(okResult.Value);
        Assert.Equal(photoDto.Id, returnedPhoto.Id);
    }

    [Fact]
    public async Task AddClothingItemPhoto_ReturnsOkResult_WithPhoto()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        var clothingItemId = Guid.NewGuid();
        var uploadResult = new ImageUploadResult
            { SecureUrl = new Uri("http://example.com/photo.jpg"), PublicId = "publicId" };
        var photoDto = new ClothingItemPhotoDto
            { Id = Guid.NewGuid(), Url = "http://example.com/photo.jpg", IsMain = true };

        _photoServiceMock.Setup(service => service.AddPhotoAsync(fileMock.Object))
            .ReturnsAsync(uploadResult);
        _clothingItemServiceMock.Setup(service => service.AddPhotoByClothingItem(uploadResult, clothingItemId))
            .ReturnsAsync(photoDto);

        // Act
        var result = await _controller.AddClothingItemPhoto(clothingItemId, fileMock.Object);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPhoto = Assert.IsType<ClothingItemPhotoDto>(okResult.Value);
        Assert.Equal(photoDto.Id, returnedPhoto.Id);
    }

    [Fact]
    public async Task SetMainClothingItemPhoto_ReturnsNoContentResult()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var photoId = Guid.NewGuid();

        // Act
        var result = await _controller.SetMainClothingItemPhoto(clothingItemId, photoId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteClothingItemPhoto_ReturnsNoContentResult()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var photoId = Guid.NewGuid();

        // Act
        var result = await _controller.DeleteClothingItemPhoto(clothingItemId, photoId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}