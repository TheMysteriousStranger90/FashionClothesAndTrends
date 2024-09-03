using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Specifications;
using FashionClothesAndTrends.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class ClothingControllerTests
{
    private readonly Mock<IClothingItemService> _mockClothingItemService;
    private readonly ClothingController _controller;
    private readonly IMapper _mapper;

    public ClothingControllerTests()
    {
        _mockClothingItemService = new Mock<IClothingItemService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClothingBrand, ClothingBrandDto>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _controller = new ClothingController(_mockClothingItemService.Object);
    }

    [Fact]
    public async Task GetClothingItem_ReturnsOkResult_WhenItemExists()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var clothingItemDto = new ClothingItemDto { Id = clothingItemId, Name = "Test Item" };
        _mockClothingItemService.Setup(service => service.GetClothingItemById(clothingItemId))
            .ReturnsAsync(clothingItemDto);

        // Act
        var result = await _controller.GetClothingItem(clothingItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ClothingItemDto>(okResult.Value);
        Assert.Equal(clothingItemId, returnValue.Id);
    }

    [Fact]
    public async Task GetClothingItem_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        _mockClothingItemService.Setup(service => service.GetClothingItemById(clothingItemId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetClothingItem(clothingItemId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task GetClothingItems_ReturnsOkResult_WhenItemsExist()
    {
        // Arrange
        var clothingSpecParams = new ClothingSpecParams();
        var clothingItems = new Pagination<ClothingItemDto>(1, 10, 1,
            new List<ClothingItemDto> { new ClothingItemDto { Id = Guid.NewGuid(), Name = "Test Item" } });
        _mockClothingItemService.Setup(service => service.GetClothingItems(clothingSpecParams))
            .ReturnsAsync(clothingItems);

        // Act
        var result = await _controller.GetClothingItems(clothingSpecParams);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Pagination<ClothingItemDto>>(okResult.Value);
        Assert.Single(returnValue.Data);
    }

    [Fact]
    public async Task GetClothingItems_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var clothingSpecParams = new ClothingSpecParams();
        _mockClothingItemService.Setup(service => service.GetClothingItems(clothingSpecParams))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetClothingItems(clothingSpecParams);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task GetClothingBrandS_ReturnsOkResult_WhenBrandsExist()
    {
        // Arrange
        var clothingBrands = new List<ClothingBrand>
        {
            new ClothingBrand { Id = Guid.NewGuid(), Name = "Test Brand" }
        };
        var clothingBrandDtos = _mapper.Map<IReadOnlyList<ClothingBrandDto>>(clothingBrands);

        _mockClothingItemService.Setup(service => service.GetClothingBrands())
            .ReturnsAsync(clothingBrandDtos);

        // Act
        var result = await _controller.GetClothingBrandS();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<ClothingBrandDto>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal(clothingBrandDtos[0].Name, returnValue[0].Name);
    }

    [Fact]
    public async Task GetClothingBrandS_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        _mockClothingItemService.Setup(service => service.GetClothingBrands())
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetClothingBrandS();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}