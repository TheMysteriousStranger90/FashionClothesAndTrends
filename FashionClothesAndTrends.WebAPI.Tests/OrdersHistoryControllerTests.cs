using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class OrdersHistoryControllerTests
{
    private readonly Mock<IOrderHistoryService> _orderHistoryServiceMock;
    private readonly OrdersHistoryController _controller;

    public OrdersHistoryControllerTests()
    {
        _orderHistoryServiceMock = new Mock<IOrderHistoryService>();
        _controller = new OrdersHistoryController(_orderHistoryServiceMock.Object);
    }
    
    [Fact]
    public async Task GetOrderHistoriesForUser_ReturnsInternalServerError_OnException()
    {
        // Arrange
        var userId = "test_user";
        _orderHistoryServiceMock.Setup(service => service.GetOrderHistoriesByUserIdAsync(userId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetOrderHistoriesByUserId();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetOrderHistoryById_ReturnsOkResult_WithOrderHistory()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderHistory = new OrderHistoryToReturnDto { Id = orderId, TotalAmount = 100, Status = OrderStatus.Pending.ToString() };
        _orderHistoryServiceMock.Setup(service => service.GetOrderHistoryByIdAsync(orderId))
            .ReturnsAsync(orderHistory);

        // Act
        var result = await _controller.GetOrderHistoryById(orderId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrderHistory = Assert.IsType<OrderHistoryToReturnDto>(okResult.Value);
        Assert.Equal(orderId, returnedOrderHistory.Id);
    }

    [Fact]
    public async Task GetOrderHistoryById_ReturnsNotFoundResult_WhenOrderHistoryNotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _orderHistoryServiceMock.Setup(service => service.GetOrderHistoryByIdAsync(orderId))
            .ThrowsAsync(new NotFoundException($"Order history with ID '{orderId}' not found."));

        // Act
        var result = await _controller.GetOrderHistoryById(orderId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
        Assert.Equal($"Order history with ID '{orderId}' not found.", apiResponse.Message);
    }

    [Fact]
    public async Task GetOrderHistoryById_ReturnsInternalServerError_OnException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _orderHistoryServiceMock.Setup(service => service.GetOrderHistoryByIdAsync(orderId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetOrderHistoryById(orderId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }

    [Fact]
    public async Task GetAllOrderHistories_ReturnsOkResult_WithOrderHistories()
    {
        // Arrange
        var orderHistories = new List<OrderHistoryToReturnDto>
        {
            new OrderHistoryToReturnDto { Id = Guid.NewGuid(), TotalAmount = 100, Status = OrderStatus.Pending.ToString() },
            new OrderHistoryToReturnDto { Id = Guid.NewGuid(), TotalAmount = 200, Status = OrderStatus.PaymentReceived.ToString() }
        };
        _orderHistoryServiceMock.Setup(service => service.GatAllOrderHistoriesAsync())
            .ReturnsAsync(orderHistories);

        // Act
        var result = await _controller.GetAllOrderHistories();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrderHistories = Assert.IsType<List<OrderHistoryToReturnDto>>(okResult.Value);
        Assert.Equal(orderHistories.Count, returnedOrderHistories.Count);
    }

    [Fact]
    public async Task GetAllOrderHistories_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _orderHistoryServiceMock.Setup(service => service.GatAllOrderHistoriesAsync())
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetAllOrderHistories();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(statusCodeResult.Value);
        Assert.Equal(500, apiResponse.StatusCode);
        Assert.Equal("An error occurred while processing your request", apiResponse.Message);
    }
}