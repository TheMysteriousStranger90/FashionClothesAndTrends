using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class BasketControllerTests
{
    private readonly Mock<IBasketService> _basketServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly BasketController _controller;

    public BasketControllerTests()
    {
        _basketServiceMock = new Mock<IBasketService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new BasketController(_basketServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetBasketById_ReturnsOkResult_WithBasket()
    {
        // Arrange
        var basketId = "test_basket";
        var basket = new CustomerBasket(basketId);
        _basketServiceMock.Setup(service => service.GetBasketAsync(basketId))
            .ReturnsAsync(basket);

        // Act
        var result = await _controller.GetBasketById(basketId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBasket = Assert.IsType<CustomerBasket>(okResult.Value);
        Assert.Equal(basketId, returnedBasket.Id);
    }

    [Fact]
    public async Task GetBasketById_ReturnsNewBasket_WhenBasketNotFound()
    {
        // Arrange
        var basketId = "test_basket";
        _basketServiceMock.Setup(service => service.GetBasketAsync(basketId))
            .ReturnsAsync((CustomerBasket)null);

        // Act
        var result = await _controller.GetBasketById(basketId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBasket = Assert.IsType<CustomerBasket>(okResult.Value);
        Assert.Equal(basketId, returnedBasket.Id);
    }

    [Fact]
    public async Task GetBasketById_ReturnsInternalServerError_OnException()
    {
        // Arrange
        var basketId = "test_basket";
        _basketServiceMock.Setup(service => service.GetBasketAsync(basketId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetBasketById(basketId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task UpdateBasket_ReturnsOkResult_WithUpdatedBasket()
    {
        // Arrange
        var basketDto = new CustomerBasketDto { Id = "test_basket" };
        var basket = new CustomerBasket(basketDto.Id);
        var updatedBasket = new CustomerBasket(basketDto.Id)
        {
            Items = new List<BasketItem> { new BasketItem { ClothingName = "Test Item", Price = 100, Quantity = 1 } }
        };

        _mapperMock.Setup(mapper => mapper.Map<CustomerBasket>(basketDto)).Returns(basket);
        _basketServiceMock.Setup(service => service.UpdateBasketAsync(basket)).ReturnsAsync(updatedBasket);

        // Act
        var result = await _controller.UpdateBasket(basketDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBasket = Assert.IsType<CustomerBasket>(okResult.Value);
        Assert.Equal(updatedBasket.Id, returnedBasket.Id);
        Assert.Single(returnedBasket.Items);
    }

    [Fact]
    public async Task UpdateBasket_ReturnsInternalServerError_OnException()
    {
        // Arrange
        var basketDto = new CustomerBasketDto { Id = "test_basket" };
        var basket = new CustomerBasket(basketDto.Id);

        _mapperMock.Setup(mapper => mapper.Map<CustomerBasket>(basketDto)).Returns(basket);
        _basketServiceMock.Setup(service => service.UpdateBasketAsync(basket))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.UpdateBasket(basketDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task DeleteBasketAsync_ReturnsNoContentResult()
    {
        // Arrange
        var basketId = "test_basket";
        _basketServiceMock.Setup(service => service.DeleteBasketAsync(basketId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBasketAsync(basketId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBasketAsync_ReturnsInternalServerError_OnException()
    {
        // Arrange
        var basketId = "test_basket";
        _basketServiceMock.Setup(service => service.DeleteBasketAsync(basketId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteBasketAsync(basketId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }
}