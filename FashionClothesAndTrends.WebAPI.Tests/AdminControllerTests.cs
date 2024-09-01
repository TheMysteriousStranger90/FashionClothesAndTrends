using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class AdminControllerTests
{
    private readonly Mock<IAdminService> _adminServiceMock;
    private readonly AdminController _controller;

    public AdminControllerTests()
    {
        _adminServiceMock = new Mock<IAdminService>();
        _controller = new AdminController(_adminServiceMock.Object);
    }

    [Fact]
    public async Task GetRoles_ReturnsOkResult_WithRoles()
    {
        // Arrange
        var roles = new List<string> { "Admin", "Buyer" };
        _adminServiceMock.Setup(service => service.GetRoles())
            .ReturnsAsync(roles);

        // Act
        var result = await _controller.GetRoles();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedRoles = Assert.IsType<List<string>>(okResult.Value);
        Assert.Equal(roles.Count, returnedRoles.Count);
    }

    [Fact]
    public async Task AddRole_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var roleName = "NewRole";
        _adminServiceMock.Setup(service => service.AddRole(roleName))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.AddRole(roleName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var isSuccess = Assert.IsType<bool>(okResult.Value);
        Assert.True(isSuccess);
    }

    [Fact]
    public async Task AddRole_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var roleName = "NewRole";
        _adminServiceMock.Setup(service => service.AddRole(roleName))
            .ThrowsAsync(new Exception("Error adding role"));

        // Act
        var result = await _controller.AddRole(roleName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error adding role", badRequestResult.Value);
    }

    [Fact]
    public async Task EditRoles_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var username = "user1";
        var roles = "Admin,Buyer";
        _adminServiceMock.Setup(service => service.EditRoles(username, roles))
            .ThrowsAsync(new Exception("Error editing roles"));

        // Act
        var result = await _controller.EditRoles(username, roles);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error editing roles", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteRole_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var roleName = "RoleToDelete";
        _adminServiceMock.Setup(service => service.DeleteRole(roleName))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteRole(roleName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var isSuccess = Assert.IsType<bool>(okResult.Value);
        Assert.True(isSuccess);
    }

    [Fact]
    public async Task DeleteRole_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var roleName = "RoleToDelete";
        _adminServiceMock.Setup(service => service.DeleteRole(roleName))
            .ThrowsAsync(new Exception("Error deleting role"));

        // Act
        var result = await _controller.DeleteRole(roleName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error deleting role", badRequestResult.Value);
    }
}